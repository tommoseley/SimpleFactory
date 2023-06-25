using System;
using System.Collections;
using SimpleFactory;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.IO;
//using SimpleFactory.Components;
//using SimpleFactory.Blueprints;
public static class Runner
{
    public static void Main(String[] Args)
    {
        // See https://aka.ms/new-console-template for more information
        Console.WriteLine("Hello, World!");
        string? val;

        List<InventoryItem> inventory = new();
        List<Blueprint> blueprints = new();
        string fileName = "blueprints.json";
        string jsonString = File.ReadAllText(fileName);
        blueprints = JsonSerializer.Deserialize<List<Blueprint>>(jsonString);
        fileName = "data.json";
        inventory = JsonSerializer.Deserialize<List<InventoryItem>>(jsonString);

        Blueprint SteelPlate = blueprints.Find(x => x.Name == "Steel Plate");

        InventoryItem Carbon = new("Carbon", 2);
        InventoryItem SteelBlock = new("Steel Block", 1);
      
        inventory.Add(Carbon);
        inventory.Add(SteelBlock);
        bool canMake = SteelPlate.CanMake(inventory);
        Console.WriteLine(String.Format("can make it: {0}", canMake));
        Carbon.Add(4);
        SteelBlock.Add(2);
        canMake = SteelPlate.CanMake(inventory);
        Console.WriteLine("can make it: {0}", canMake);
        if (SteelPlate.CanMake(inventory))
            inventory.Add(SteelPlate.Make(inventory));
        fileName = "data.json";
        jsonString = JsonSerializer.Serialize(inventory);

        File.WriteAllText(fileName, jsonString);
        Console.WriteLine(jsonString);
        Console.WriteLine("Press any key to exit.");

    }
}
