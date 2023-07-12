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
        ComponentCollection FactoryInventory = new ComponentCollection() { Name = "Factory Inventory" };
        InventoryRegion region = new InventoryRegion (Console.WindowWidth / 2, 0, ConsoleColor.Yellow, FactoryInventory); 

        while (true)
        {
            region.UpdateText();
            Console.WriteLine(
                "Enter a command: add <count> <item>, make <item>");
            Console.WriteLine(
                "                 machines, exit");
            Console.Write(">");
            val = Console.ReadLine();
            if (val == null)
                break;
            if (val == "exit")
                break;
            Console.Clear();
            string[] parts = val.Split(' ');
            switch (parts[0])
            {
                case "add":
                    if (parts.Length > 2)
                    {
                        if (int.TryParse(parts[1], out int count))
                        {
                            StringBuilder stringBuilder = new StringBuilder();  
                            for (int i = 2; i < parts.Length; i++)
                            {
                                stringBuilder.Append(parts[i].ToUpperInvariant());
                                if (i < parts.Length - 1)
                                    stringBuilder.Append(" ");
                            }
                            FactoryInventory.Add(stringBuilder.ToString(), count);
                        }
                    }
                    break;
                case "make":
                    if (parts.Length == 2)
                    {
                        machines[0].Hopper = FactoryInventory;
                        if (machines[0].CanMake(parts[1]))
                        {
                            machines[0].Make(parts[1]);
                        }
                    }
                    break;
                case "machines":
                    ShowMachines(machines[0]);
                    break;
                default:
                    Console.WriteLine(String.Format("You entered: {0}", val));
                    break;
            }
        }
    }


    public static void ShowMachines(Machine machine)
    {
        int left = Console.CursorLeft;
        int top = Console.CursorTop;
        Console.ForegroundColor = ConsoleColor.Cyan;

        int LineNumber = 10;
        Console.SetCursorPosition(0, LineNumber++);

        Console.WriteLine("Machines:");
        Console.SetCursorPosition(0, LineNumber++);
        Console.WriteLine("---------------");
        Console.SetCursorPosition(0, LineNumber++);
        Console.WriteLine("Name:     {0}", machine.Name);
        Console.SetCursorPosition(0, LineNumber);
        Console.Write("Recipes:");
        Console.SetCursorPosition(10, LineNumber++);
        foreach (string key in machine.Blueprints.Keys)
        {
            Console.WriteLine(String.Format("{0}", machine.Blueprints[key].Name));
            Console.SetCursorPosition(10, LineNumber++);
        }

        Console.SetCursorPosition(left, top);
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine();
    }
    public static void CreateMachines (List<Machine> machines)
    {
        Machine Assembler = new Machine() { Name = "Assembler" };
        Blueprint SteelPlate = new Blueprint("SteelPlate",
            new Dictionary<string, int>() { { "Carbon", 4 }, { "Steel Block", 2 } });
        Assembler.Blueprints.Add(SteelPlate.Name, SteelPlate);
        Blueprint Money = new Blueprint("Money",
            new Dictionary<string, int>() { { "Carbon", 4 } });
        Assembler.Blueprints.Add(Money.Name, Money);
        machines.Add(Assembler);
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
