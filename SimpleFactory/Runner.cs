using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using System.Xml.Linq;
using SimpleFactory.Regions;
using System.Timers;
using SimpleFactory.CommandHandlers;
using System.Reflection.Metadata.Ecma335;

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
            Status status = new Status();
            FactoryInventory = new Inventory() { Name = "Factory Inventory" };
            FactoryInventory.Create();
            string? val;
            regions.Add(new ActionRegion() { X = 0, Y = 0, Width = (Console.WindowWidth / 2) - 1, Height = 6, IsVisible = true, Color = ConsoleColor.White });
            regions.Add(new StatusRegion() { X = 0, Y = 7, Width = (Console.WindowWidth / 2) - 1, Height = Console.WindowHeight - 12, IsVisible = true, Color = ConsoleColor.Green, ErrorColor = ConsoleColor.Red });
            regions.Add(new HeaderRegion() { X = Console.WindowWidth / 2, Y = 0, Width = (Console.WindowWidth / 2) - 1, Height = 5, IsVisible = true, Color = ConsoleColor.White });
            inventoryRegion = new InventoryRegion() { X = Console.WindowWidth / 2, Y = 6, Width = Console.WindowWidth / 2, Height = Console.WindowHeight - 8, IsVisible = true, Color = ConsoleColor.Yellow, Parts = FactoryInventory };    
            regions.Add(inventoryRegion);
            machinesRegion = new MachinesRegion() { X = Console.WindowWidth / 2, Y = 6, Width = Console.WindowWidth / 2, Height = Console.WindowHeight - 8, IsVisible = false, Color = ConsoleColor.Cyan };
            regions.Add(machinesRegion);
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
                ResetConsole();
                val = Console.ReadLine();
                if (string.IsNullOrEmpty(val)) { continue; }
                Command command = CommandFactory.Instance.Get(val);
                Command.Result result = command.Execute();
                if (result == Command.Result.End)
                {
                    break;
                }
            }
        }
        private void ResetConsole()
        {
            foreach (Region region in regions)
                region.ClearRegion();
            foreach (Region region in regions)
                if (region.IsVisible == true)
                    region.UpdateRegion();
            Console.SetCursorPosition(3, 3);

        }
        private void Action_OnCommand(object sender, CommandEventArgs action)
        {
            //Console.WriteLine($"CommandHandlers: {action.Command} {action.Count} {action.Item}");
            //switch (action.Command)
            //{
            //    case "exit":
            //        return;
            //    case "inv":
            //        machinesRegion.IsVisible = false;
            //        inventoryRegion.IsVisible = true;
            //        return;
            //    case "mac":
            //        machinesRegion.IsVisible = true;
            //        inventoryRegion.IsVisible = false;
            //        return;
            //    case "buy":
            //        if (action.Item == string.Empty)
            //        {
            //            Status.Current.Set($"Nothing to build: {action.RawText}", false);
            //            return;
            //        }
            //        if (!FactoryInventory.Items.ContainsKey(action.Item))
            //        {
            //            Status.Current.Set($"Item {action.Item} not found", false);
            //            break;
            //        }
            //        if (action.Command == "buy")
            //        {
            //            Console.WriteLine($"Buy {action.Count} {action.Item} for {FactoryInventory.Items[action.Item].CostBasis} each?");
            //            Console.Write("(y/n) >");
            //            if (Console.ReadLine() == "y")
            //            {
            //                System.Timers.Timer timer = new System.Timers.Timer(5000);
            //                int totalSpent = action.Count * FactoryInventory.Items[action.Item].CostBasis;
            //                Status.Current.Set($"Bought {action.Count} {action.Item} for {totalSpent}", true);
            //                timer.Elapsed += (sender, e) =>
            //                {
            //                    FactoryInventory.Add(action.Item, action.Count);
            //                    timer.Stop();
            //                    Status.Current.Set($"{action.Count} {action.Item} delivered", true);
            //                    ResetConsole();
            //                };
            //                timer.Start();
            //            }
            //        }
            //        break;
            //    case "make":
            //        if (action.Item == string.Empty)
            //        {
            //            Status.Current.Set($"Nothing to build: {action.RawText}", false);
            //            return;
            //        }
            //        Machine machine = Machine.WhatCanMakePlan(action.Item);
            //        if (machine != null)
            //        {
            //            machine.Hopper = FactoryInventory;
            //            if (machine.CanProduce(action.Item))
            //            {
            //                machine.Make(action.Item);
            //                Status.Current.Set($"Built {action.Item}", true);
            //            }
            //            else
            //            {
            //                Status.Current.Set($"No resources to build {action.Item}", false);
            //            }
            //        }
            //        else
            //        {
            //            Status.Current.Set($"Cannot build {action.Item}", false);
            //        }
            //        break;
            //    default:
            //        Status.Current.Set($"Unknown entry: {action.RawText}", false);
            //        break;
            //}
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
