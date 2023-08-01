using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using System.Xml.Linq;
using SimpleFactory.Regions;
namespace SimpleFactory
{
    public class Runner
    {
        public Inventory FactoryInventory;
        public List<Region> regions = new List<Region>();
        private Region inventoryRegion, machinesRegion;
        public void Run()
        {
            string SavePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "IndustrySim");
            if (!Directory.Exists(SavePath)) { Directory.CreateDirectory(SavePath); }
            CommandAction action = new CommandAction();
            Status status = new Status();
            action.OnCommand += Action_OnCommand;
            FactoryInventory = new Inventory() { Name = "Factory Inventory" };
            FactoryInventory.Create();
            string? val;
            regions.Add(new HeaderRegion(Console.WindowWidth / 2, 0, (Console.WindowWidth / 2) - 1, 5, true, ConsoleColor.White));
            regions.Add(new StatusRegion(0, 7, (Console.WindowWidth / 2) - 1, Console.WindowHeight - 12, true, ConsoleColor.Green, ConsoleColor.Red));
            inventoryRegion = new InventoryRegion(Console.WindowWidth / 2, 6, Console.WindowWidth / 2, Console.WindowHeight - 8, true, ConsoleColor.Yellow, FactoryInventory);
            regions.Add(inventoryRegion);
            machinesRegion = new MachinesRegion(Console.WindowWidth / 2, 6, Console.WindowWidth / 2, Console.WindowHeight - 8, false, ConsoleColor.Cyan);
            regions.Add(machinesRegion);
            regions.Add(new ActionRegion(0, 0, (Console.WindowWidth / 2) - 1, 7, true, ConsoleColor.White));
            Status.Current.Set("Factory Started", true);
            Status.Current.Set("Inventory Started", true);
            Plan.Create();
            Machine.Create();
            Machine.AssignPlans();
            Status.Current.Set("Machines Started", true);
            Status.Current.Set("Actions Started", true);
            Console.WriteLine("Welcome to IndustrySim");
            while (true)
            {
                foreach (Region region in regions)
                    region.ClearRegion();
                foreach (Region region in regions)
                    if (region.IsVisible == true)
                        region.UpdateText(); 
                val = Console.ReadLine();
                if (string.IsNullOrEmpty(val)) { continue; }
                action.Parse(val);
                if (action.Command == "exit")
                {
                    Console.Clear();
                    break;
                }
            }
        }
        private void Action_OnCommand(object sender, CommandEventArgs action)
        {
            Console.WriteLine($"Command: {action.Command} {action.Count} {action.Item}");
            switch (action.Command)
            {
                case "exit":
                    return;
                case "inv":
                    machinesRegion.IsVisible = false;
                    inventoryRegion.IsVisible = true;
                    return;
                case "mac":
                    machinesRegion.IsVisible = true;
                    inventoryRegion.IsVisible = false;
                    return;
                case "buy":
                    if (action.Item == string.Empty)
                    {
                        Status.Current.Set($"Nothing to build: {action.RawText}", false);
                        return;
                    }
                    if (!FactoryInventory.Items.ContainsKey(action.Item))
                    {
                        Status.Current.Set($"Item {action.Item} not found", false);
                        break;
                    }
                    if (action.Command == "buy")
                    {
                        Console.WriteLine($"Buy {action.Count} {action.Item} for {FactoryInventory.Items[action.Item].CostBasis} each?");
                        Console.Write("(y/n) >");
                        if (Console.ReadLine() == "y")
                        {
                            int totalSpent = action.Count * FactoryInventory.Items[action.Item].CostBasis;
                            FactoryInventory.Add(action.Item, action.Count);
                            Status.Current.Set($"Bought {action.Count} {action.Item} for {totalSpent}", true);
                        }
                    }
                    break;
                case "make":
                    if (action.Item == string.Empty)
                    {
                        Status.Current.Set($"Nothing to build: {action.RawText}", false);
                        return;
                    }
                    Machine machine = Machine.WhatCanMakePlan(action.Item);
                    if (machine != null)
                    {
                        machine.Hopper = FactoryInventory;
                        if (machine.CanProduce(action.Item))
                        {
                            machine.Make(action.Item);
                            Status.Current.Set($"Built {action.Item}", true);
                        }
                        else
                        {
                            Status.Current.Set($"No resources to build {action.Item}", false);
                        }
                    }
                    else
                    {
                        Status.Current.Set($"Cannot build {action.Item}", false);
                    }
                    break;
                default:
                    Status.Current.Set($"Unknown entry: {action.RawText}", false);
                    break;
            }
        }
        public delegate void OnStatusChangedHandler(object sender, Status status);
        public class StatusChangeEventArgs : EventArgs
        {
            public string Message { get; set; }
            public DateTime Timestamp { get; set; }
            public bool isSuccessful { get; set; }
            public StatusChangeEventArgs(string message, DateTime timestamp, bool isSuccessful)
            {
                Message = message;
                Timestamp = timestamp;
                this.isSuccessful = isSuccessful;
            }
        }

    }
}
