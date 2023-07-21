﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.IO;
using SimpleFactory.Machines;
//using SimpleFactory.Components;
//using SimpleFactory.Blueprints;
using SimpleFactory.Components;
using System.Runtime.CompilerServices;
using SimpleFactory.Blueprints;
using SimpleFactory.Regions;
using SimpleFactory.Status;

public static class Runner
{
    public static void Main(String[] Args)
    {
        string SavePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "IndustrySim");
        if (!Directory.Exists(SavePath)) { Directory.CreateDirectory(SavePath); }
        
        ComponentCollection FactoryInventory = new ComponentCollection() { Name = "Factory Inventory" };
        string? val;
       
        ComponentFactory.LoadComponentsFromJSON(Path.Combine(SavePath, "Components.json"));
        ComponentFactory.CreateBlueprints();
        SortedList<string, Machine> machines = CreateMachines(FactoryInventory);
        List<Region> regions = new List<Region>();
        StatusContent status = new StatusContent();

        regions.Add (new InventoryRegion (Console.WindowWidth / 2, 0, Console.WindowWidth / 2, 12, ConsoleColor.Yellow, FactoryInventory));
        regions.Add (new MachinesRegion(Console.WindowWidth / 2, 15, Console.WindowWidth / 2, Console.WindowHeight - 17, ConsoleColor.Cyan, machines));
        regions.Add (new StatusRegion (0, 5, (Console.WindowWidth / 2)-1, Console.WindowHeight - 12, ConsoleColor.Green, ConsoleColor.Red, status));
        status.SetStatus("Factory Started", true);
        Console.WriteLine(
            "Enter a command: buy <count> <item>, make <item>, exit");
        Console.Write(">");
        while (true)
        {
            foreach (Region region in regions)
                region.UpdateRegionText();
            Console.CursorTop = 1;
            Console.CursorLeft = 2;
            //clear line and write prompt
            Console.Write(new string(' ', Console.WindowWidth /2 - 2));
            Console.CursorTop = 1;
            Console.CursorLeft = 2;
            val = Console.ReadLine();
            if (val == null)
                break;
            if (val == "exit")
                break;
            string[] parts = val.Split(' ');
            
            switch (parts[0])
            {
                case "buy":
                case "make":
                    if (parts.Length > 1)
                    {
                        int startIndex = 1;
                        if (int.TryParse(parts[1], out int count))
                        {
                            startIndex = 2;
                        }
                        else
                        {
                            count = 1;
                        }
                        StringBuilder stringBuilder = new StringBuilder();
                        for (int i = startIndex; i < parts.Length; i++)
                        {
                            stringBuilder.Append(parts[i]);
                            if (i < parts.Length - 1)
                                stringBuilder.Append(" ");
                        }
                        Component addedComponent = ComponentFactory.GetComponent(stringBuilder.ToString().Trim());
                        if (addedComponent == null)
                        {
                            status.SetStatus(string.Format("Component {0} not found", stringBuilder.ToString()), false);
                            break;
                        }
                        if (parts[0] == "buy")
                        {
                            if (addedComponent.Blueprint != null)
                            {
                                status.SetStatus(string.Format("Component {0} must be built", stringBuilder.ToString()), false);
                            }
                            else
                            {
                                FactoryInventory.Add(addedComponent, count);
                                status.SetStatus(string.Format("Bought {0} {1}", count, stringBuilder.ToString()), true);
                            }
                        }
                        else
                        {
                            Machine machine = machines.Values.First<Machine>(m => m.Produces.Contains(addedComponent));
                            if (machine != null)
                            {
                                machine.Hopper = FactoryInventory;
                                if (addedComponent.Blueprint.CanMake(machine.Hopper))
                                {
                                    addedComponent.Blueprint.Make(machine.Hopper);
                                    status.SetStatus(string.Format("Built {0}", stringBuilder.ToString()), true);
                                }
                                else
                                {
                                    status.SetStatus(string.Format("No resources to build {0}", stringBuilder.ToString()), false);
                                }
                            }
                            else
                            {
                                status.SetStatus(string.Format("Cannot build {0}", stringBuilder.ToString()), false);
                            }

                        }
                    }
                    break;
                default:
                    status.SetStatus(string.Format("Unknown entry: {0}", val), false);
                    break;
            }
        }
        ComponentFactory.SaveComponentsToJSON(Path.Combine(SavePath, "Components.json")) ;
        SaveInventory(FactoryInventory, Path.Combine(SavePath, "Inventory.json"));
    }

    public static SortedList<string, Machine> CreateMachines (ComponentCollection inventory)
    {
        SortedList<string, Machine> machines = new SortedList<string, Machine>();
        Machine machine = new Machine() { Name = "Steel Maker" };
        Component makes = ComponentFactory.GetComponent("Steel Block");
        machine.Produces.Add(makes);
        machines.Add (machine.Name, machine);
        machine = new Machine() { Name = "Steel Pounder" };
        makes = ComponentFactory.GetComponent("Steel Plate");
        machine.Produces.Add(makes);
        machines.Add(machine.Name, machine);
        machine = new Machine() { Name = "Steel Sheeter" };
        makes = ComponentFactory.GetComponent("Steel Sheet");
        machine.Produces.Add(makes);
        machines.Add(machine.Name, machine);
        return machines;
    }

    public static string LoadInventory(string fileName)
    {

        return File.ReadAllText(fileName);
    }

    public static void SaveInventory(ComponentCollection FactoryInventory, string fileName)
    {
        string json = JsonSerializer.Serialize(FactoryInventory);
        System.IO.File.WriteAllText(fileName, json);
    }
}
