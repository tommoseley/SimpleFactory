using System;
using System.Collections;
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

public static class Runner
{
    public static void Main(String[] Args)
    {
        List<Machine> machines = new List<Machine>();
        string? val;
        ComponentFactory.CreateComponents();
        ComponentFactory.CreateBlueprints();
        CreateMachines(machines);
        List<Region> regions = new List<Region>();

        ComponentCollection FactoryInventory = new ComponentCollection() { Name = "Factory Inventory" };
        regions.Add (new InventoryRegion (Console.WindowWidth / 2, 0, ConsoleColor.Yellow, FactoryInventory));
        regions.Add (new MachinesRegion(Console.WindowWidth / 2, 15, ConsoleColor.Cyan, machines));
        // Component SteelPlate = ComponentFactory.GetComponent("Steel Plate");
        Status status = new Status();
        while (true)
        {
            foreach (Region region in regions)
                region.UpdateRegionText();
            Console.WriteLine(
                "Enter a command: add <count> <item>, make <item>, exit");
            Console.Write(">");
            val = Console.ReadLine();
            if (val == null)
                break;
            if (val == "exit")
                break;
            string[] parts = val.Split(' ');
            
            switch (parts[0])
            {
                case "add":
                    if (parts.Length > 2)
                    {
                        int startIndex = 1;

                        if (int.TryParse(parts[1], out int count))
                        {
                            startIndex = 2;
                        }
                        else
                            count = 1;
                        StringBuilder stringBuilder = new StringBuilder();
                        for (int i = startIndex; i < parts.Length; i++)
                        {
                            stringBuilder.Append(parts[i]);
                            if (i < parts.Length - 1)
                                stringBuilder.Append(" ");
                        }
                        Component addedComponent = ComponentFactory.GetComponent(stringBuilder.ToString().Trim());
                        if (addedComponent.Blueprint != null)
                        {
                            StatusMessage
                            StatusMessage(string.Format("   Component {0} had a blueprint and must be built", stringBuilder.ToString()), false);
                        }
                        else if (addedComponent != null)
                        {
                            FactoryInventory.Add(addedComponent, count);
                            StatusMessage(string.Format("   Acquired {0} {1}", count, stringBuilder.ToString()), true);
                        }
                        else
                        {
                            StatusMessage(string.Format("   Component {0} not found", stringBuilder.ToString()), false);
                        }
                    }
                    break;
                case "make":
                    if (parts.Length >1)
                    {
                        StringBuilder stringBuilder = new StringBuilder();
                        for (int i = 1; i < parts.Length; i++)
                        {
                            stringBuilder.Append(parts[i]);
                            if (i < parts.Length - 1)
                                stringBuilder.Append(" ");
                        }
                        Component toMake = ComponentFactory.GetComponent(stringBuilder.ToString());
                        if (toMake != null)
                        {
                            if (machines[0].Produces.Contains(toMake))
                            {
                                machines[0].Hopper = FactoryInventory;
                                if (toMake.Blueprint.CanMake(FactoryInventory))
                                {
                                    toMake.Blueprint.Make(FactoryInventory);
                                    StatusMessage(string.Format("   Component {0} made", stringBuilder.ToString()), true);
                                }
                                else
                                {
                                    StatusMessage(string.Format("   Component {0} cannot be made", stringBuilder.ToString()), false);
                                }
                            }
                        }
                    }
                    break;
                default:
                    Console.WriteLine(String.Format("You entered: {0}", val));
                    break;
            }
        }
    }

    private static void StatusMessage(string message, bool success)
    {
        ConsoleColor oldColor1 = Console.ForegroundColor;
        Console.ForegroundColor = success ? ConsoleColor.Green : ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ForegroundColor = oldColor1;
    }
    public static void CreateMachines (List<Machine> machines)
    {
        Machine Assembler = new Machine() { Name = "Assembler" };
        Component SteelPlate = ComponentFactory.GetComponent("Steel Plate");
        Assembler.Produces.Add(SteelPlate);
        machines.Add   (Assembler);
    }

    public static string LoadJson(string fileName)
    {
        return File.ReadAllText(fileName);
    }

    public static void SaveJson(string fileName, string jsonString)
    {
        File.WriteAllText(fileName, jsonString);
    }
}
