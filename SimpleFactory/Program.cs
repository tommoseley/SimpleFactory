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
using SimpleFactory.Status;

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
        StatusContent status = new StatusContent();

        ComponentCollection FactoryInventory = new ComponentCollection() { Name = "Factory Inventory" };
        regions.Add (new InventoryRegion (Console.WindowWidth / 2, 0, Console.WindowWidth / 2, 12, ConsoleColor.Yellow, FactoryInventory));
        regions.Add (new MachinesRegion(Console.WindowWidth / 2, 15, Console.WindowWidth / 2, Console.WindowHeight - 17, ConsoleColor.Cyan, machines));
        regions.Add (new StatusRegion (0, 5, (Console.WindowWidth / 2)-1, Console.WindowHeight - 12, ConsoleColor.Green, ConsoleColor.Red, status));
        status.SetStatus("Factory Started", true);
        Console.WriteLine(
            "Enter a command: buy <count> <item>, build <item>, exit");
        Console.Write(">");
        while (true)
        {
            foreach (Region region in regions)
                region.UpdateRegionText();
            Console.CursorTop = 1;
            Console.CursorLeft = 2;
            //clear line and write prompt
            Console.Write(new string(' ', Console.WindowWidth /2 - 2));
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
                case "build":
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
                            if (machines[0].Produces.Contains(addedComponent))
                            {
                                machines[0].Hopper = FactoryInventory;
                                if (addedComponent.Blueprint.CanMake(FactoryInventory))
                                {
                                    addedComponent.Blueprint.Make(FactoryInventory);
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
