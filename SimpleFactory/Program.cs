using System;
using System.Collections;
using SimpleFactory;
//using SimpleFactory.Components;
//using SimpleFactory.Blueprints;
public static class Runner
{
    public static void Main(String[] Args)
    {
        // See https://aka.ms/new-console-template for more information
        Console.WriteLine("Hello, World!");
        string? val;
        ComponentCollection inventory = new();
        List<Blueprint> Blueprints = new();
        Component SteelBlock = new()
        {
            Name = "Steel Block"
        };
        Component SteelPlate = new()
        {
            Name = "Steel Plate",
        };
        SteelPlate.Recipe.Add(SteelBlock, 2);
        Component result;
        inventory.Add(SteelBlock);
        bool canMake = SteelPlate.CanMake(inventory);
        Console.WriteLine(String.Format("{0} can make it: {1}", inventory.ItemCount(SteelBlock), canMake));
        inventory.Add(SteelBlock);
        canMake = SteelPlate.CanMake(inventory);
        Console.WriteLine(String.Format("{0} can make it: {1}", inventory.ItemCount(SteelBlock), canMake));
        result = SteelPlate.Make(inventory);
        Console.WriteLine("Made: " + result.Name);
        do
        {
            val = Console.ReadLine();
            Console.WriteLine(val);

        } while (val != "x");
    }
}
