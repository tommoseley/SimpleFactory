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
using SimpleFactory.Status;
using SimpleFactory;

public static class Runner
{
    public static void Main(String[] Args)
    {
        string SavePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "IndustrySim");
        if (!Directory.Exists(SavePath)) { Directory.CreateDirectory(SavePath); }
        CommandAction action = new CommandAction();
        ThingCollection FactoryInventory = new ThingCollection() { Name = "Factory Inventory" };
        string? val;
       
//        Component.LoadFromJSON(Path.Combine(SavePath, "Components.json"));
        Thing.Create();
        Blueprint.Create();
        Machine.Create();
        Machine.AssignBlueprints();
        List<Region> regions = new List<Region>();
        StatusContent status = new StatusContent();

        regions.Add (new InventoryRegion (Console.WindowWidth / 2, 0, Console.WindowWidth / 2, 12, ConsoleColor.Yellow, FactoryInventory));
        regions.Add (new MachinesRegion(Console.WindowWidth / 2, 15, Console.WindowWidth / 2, Console.WindowHeight - 17, ConsoleColor.Cyan));
        regions.Add (new StatusRegion (0, 7, (Console.WindowWidth / 2)-1, Console.WindowHeight - 12, ConsoleColor.Green, ConsoleColor.Red, status));
        status.SetStatus("Factory Started", true);
        while (true)
        {
            foreach (Region region in regions)
                 region.UpdateRegionText();
            for (int line = 0; line < 7; line++)
            {
                Console.CursorTop = line;
                Console.CursorLeft = 0;
                Console.Write(new string(' ', Console.WindowWidth / 2 - 2));
            }
            Console.CursorLeft = 0;
            Console.CursorTop = 0;
            Console.WriteLine("Enter a command: buy <count> <item>");
            Console.WriteLine("                 make <recipe>");
            Console.WriteLine("                 exit");
            Console.Write(">");
            Console.CursorLeft = 2;
            val = Console.ReadLine();
            action.Parse(val);
            switch (action.Command)
            {
                case "exit":
                    return;
                case "buy":
                    if (action.Item == string.Empty)
                    {
                        status.SetStatus(string.Format("Nothing to build: {0}", val), false);
                        continue;
                    }
                    Thing addedThing = Thing.Get(action.Item);
                    if (addedThing == null)
                    {
                        status.SetStatus(string.Format("Thing {0} not found", action.Item), false);
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
                            status.SetStatus(string.Format("Bought {0} {1} for {2}", action.Count, action.Item, totalSpent), true);
                        }
                    }
                    break;
                case "make":
                    if (action.Item == string.Empty)
                    {
                        status.SetStatus(string.Format("Nothing to build: {0}", val), false);
                        continue;
                    }
                    Machine machine = Machine.WhatCanRunRecipe(action.Item);
                    if (machine != null)
                    {
                        machine.Hopper = FactoryInventory;
                        if (machine.CanProduce(action.Item))
                        {
                            //                                    addedThing.Blueprint.Make(machine.Hopper);
                            status.SetStatus(string.Format("Built {0}", action.Item), true);
                        }
                        else
                        {
                            status.SetStatus(string.Format("No resources to build {0}", action.Item), false);
                        }
                    }
                    else
                    {
                        status.SetStatus(string.Format("Cannot build {0}", action.Item), false);
                    }
                    break;
                default:
                    status.SetStatus(string.Format("Unknown entry: {0}", val), false);
                    break;
            }
        }
        Thing.SaveToJSON(Path.Combine(SavePath, "Components.json")) ;
    }
}
