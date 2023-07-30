using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.IO;
//using SimpleFactory.Components;
//using SimpleFactory.Blueprints;
using SimpleFactory.Regions;
using SimpleFactory;
using static System.Collections.Specialized.BitVector32;

public static class Runner
{
    static ThingCollection FactoryInventory;
    static bool Running = true;
    public static void Main(String[] Args)
    {

        string SavePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "IndustrySim");
        if (!Directory.Exists(SavePath)) { Directory.CreateDirectory(SavePath); }
        CommandAction action = new CommandAction();
        action.OnCommand += Action_OnCommand;   
        FactoryInventory = new ThingCollection() { Name = "Factory Inventory" };
        string? val;
       
//        Component.LoadFromJSON(Path.Combine(SavePath, "Components.json"));
        Thing.Create();
        Blueprint.Create();
        Machine.Create();
        Machine.AssignBlueprints();
        List<Region> regions = new List<Region>();
        Status status = new Status();

        regions.Add (new InventoryRegion (Console.WindowWidth / 2, 0, Console.WindowWidth / 2, 12, ConsoleColor.Yellow, FactoryInventory));
        regions.Add (new MachinesRegion(Console.WindowWidth / 2, 15, Console.WindowWidth / 2, Console.WindowHeight - 17, ConsoleColor.Cyan));
        regions.Add (new StatusRegion (0, 7, (Console.WindowWidth / 2)-1, Console.WindowHeight - 12, ConsoleColor.Green, ConsoleColor.Red));
        regions.Add (new ActionRegion(0, 0, (Console.WindowWidth / 2) - 1, 7, ConsoleColor.White));
        Status.Current.Set("Factory Started", true);
        while (true)
        {
            foreach (Region region in regions)
                 region.UpdateRegionText();
            val = Console.ReadLine();
            action.Parse(val);
            if (action.Command == "exit") 
            {
                Console.Clear();
                break; 
            }
        }
    }

    private static void Action_OnCommand(object sender, CommandEventArgs action)
    {
        Console.WriteLine(string.Format("Command: {0} {1} {2}", action.Command, action.Count, action.Item));
        switch (action.Command)
        {
            case "exit":
                return;
            case "buy":
                if (action.Item == string.Empty)
                {
                    Status.Current.Set(string.Format("Nothing to build: {0}", action.RawText), false);
                    return;
                }
                Thing addedThing = Thing.Get(action.Item);
                if (addedThing == null)
                {
                    Status.Current.Set(string.Format("Thing {0} not found", action.Item), false);
                    break;
                }
                if (action.Command == "buy")
                {
                    int totalSpent = action.Count * addedThing.CostBasis;
                    Console.WriteLine(string.Format("Buy {0} {1} for {2}?", action.Count, addedThing.Name, addedThing.CostBasis));
                    Console.Write("(y/n) >");
                    if (Console.ReadLine() == "y")
                    {
                        FactoryInventory.Add(addedThing, action.Count);
                        Status.Current.Set(string.Format("Bought {0} {1} for {2}", action.Count, action.Item, totalSpent), true);
                    }
                }
                break;
            case "make":
                if (action.Item == string.Empty)
                {
                    Status.Current.Set(string.Format("Nothing to build: {0}", action.RawText), false);
                    return;
                }
                Machine machine = Machine.WhatCanRunRecipe(action.Item);
                if (machine != null)
                {
                    machine.Hopper = FactoryInventory;
                    if (machine.CanProduce(action.Item))
                    {
                        machine.Make(action.Item);                                   
                        Status.Current.Set(string.Format("Built {0}", action.Item), true);
                    }
                    else
                    {
                        Status.Current.Set(string.Format("No resources to build {0}", action.Item), false);
                    }
                }
                else
                {
                    Status.Current.Set(string.Format("Cannot build {0}", action.Item), false);
                }
                break;
            default:
                Status.Current.Set(string.Format("Unknown entry: {0}", action.RawText), false);
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
