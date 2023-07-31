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
    static Inventory FactoryInventory;
    public static void Main(String[] Args)
    {

        string SavePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "IndustrySim");
        if (!Directory.Exists(SavePath)) { Directory.CreateDirectory(SavePath); }
        CommandAction action = new CommandAction();
        Status status = new Status();
        action.OnCommand += Action_OnCommand;   
        FactoryInventory = new Inventory() { Name = "Factory Inventory" };
        FactoryInventory.Create();
        string? val;
       
//        Component.LoadFromJSON(Path.Combine(SavePath, "Components.json"));
        Region.Regions.Add(new StatusRegion(0, 7, (Console.WindowWidth / 2) - 1, Console.WindowHeight - 12, ConsoleColor.Green, ConsoleColor.Red));
        Thing.Create();
        Status.Current.Set("Factory Started", true);
        Region.Regions.Add (new InventoryRegion (Console.WindowWidth / 2, 0, Console.WindowWidth / 2, 12, ConsoleColor.Yellow, FactoryInventory));
        Status.Current.Set("Inventory Started", true);
        Blueprint.Create();
        Machine.Create();
        Machine.AssignBlueprints();
        Region.Regions.Add (new MachinesRegion(Console.WindowWidth / 2, 15, Console.WindowWidth / 2, Console.WindowHeight - 17, ConsoleColor.Cyan));
        Status.Current.Set("Machines Started", true);
        Region.Regions.Add (new ActionRegion(0, 0, (Console.WindowWidth / 2) - 1, 7, ConsoleColor.White));
        Status.Current.Set("Actions Started", true);
        while (true)
        {
            Region.UpdateAllRegions();
            val = Console.ReadLine();
            if(string.IsNullOrEmpty(val)) { continue; }
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
        Console.WriteLine($"Command: {action.Command} {action.Count} {action.Item}");
        switch (action.Command)
        {
            case "exit":
                return;
            case "buy":
                if (action.Item == string.Empty)
                {
                    Status.Current.Set($"Nothing to build: {action.RawText}", false);
                    return;
                }
                if (!Thing.Exists(action.Item))
                {
                    Status.Current.Set($"Thing {action.Item} not found", false);
                    break;
                }
                if (action.Command == "buy")
                {
                    Console.WriteLine($"Buy {action.Count} {action.Item} for {Thing.CostBasis(action.Item)} each?");
                    Console.Write("(y/n) >");
                    if (Console.ReadLine() == "y")
                    {
                        int totalSpent = action.Count * Thing.CostBasis(action.Item);
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
                Machine machine = Machine.WhatCanRunRecipe(action.Item);
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
