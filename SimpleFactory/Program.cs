using System;
using System.Collections;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.IO;
using SimpleFactory.Machines;
//using SimpleFactory.Components;
//using SimpleFactory.Blueprints;
using SimpleFactory;
public static class Runner
{
    public static void Main(String[] Args)
    {
        // See https://aka.ms/new-console-template for more information
        Console.WriteLine("Hello, World!");
        string? val;
        ComponentCollection Hopper = new();
        Hopper.Add("Carbon", 4);
        Hopper.Add("Steel Block", 2);
        Machine Assembler = new Machine() { Name = "Assembler" };
        Blueprint SteelPlate = new Blueprint("Steel Plate", new Dictionary<string, int>() { { "Carbon", 4 }, { "Steel Block", 2 } });
        Assembler.Blueprints.Add(SteelPlate.Name, SteelPlate);
        Console.WriteLine(Hopper.ToString());
        while (true)
        {
            Console.WriteLine("Enter a command:");
            val = Console.ReadLine();
            if (val == null)
                break;
            if (val == "exit")
                break;
            //q:how do I parse val to get an item and count to add to the Hopper?
            string[] parts = val.Split(' ');
            if (parts.Length == 2)
            {
                if (int.TryParse(parts[1], out int count))
                {
                    Hopper.Add(parts[0], count);
                }
            }
            Console.WriteLine(String.Format("You entered: {0}", val));
        }

        //Dictionary<string, int> inventory = new();
        //inventory.Add ("Steel Block", 2);
        ////comment added!
        ////inventory.Add(Carbon);
        ////inventory.Add(SteelBlock);
        //bool canMake = Assembler.CanMake(SteelPlate.Name, inventory);
        //Console.WriteLine(String.Format("can make it: {0}", canMake));
        //inventory.Add("Carbon", 4);
        //canMake = Assembler.CanMake(SteelPlate.Name, inventory);
        //Console.WriteLine(String.Format("can make it: {0}", canMake));
        //if (canMake)
        //    Assembler.Make(SteelPlate.Name, inventory);
        //Console.ReadLine();
        ////Carbon.Add(4);
        ////SteelBlock.Add(2);
        //canMake = SteelPlate.CanMake(inventory);
        //Console.WriteLine("can make it: {0}", canMake);
        //if (SteelPlate.CanMake(inventory))
        //    inventory.Add(SteelPlate.Make(inventory));
        //fileName = "data.json";
        //jsonString = JsonSerializer.Serialize(inventory);

        //File.WriteAllText(fileName, jsonString);
        //Console.WriteLine(jsonString);
        //Console.WriteLine("Press any key to exit.");

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
