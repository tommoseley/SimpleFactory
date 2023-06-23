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
        ComponentCollection Inventory = new();
        List<Blueprint> Blueprints = new();
        Component SteelBlock = new Component
        {
            Name = "Steel Block"
        };
        Component SteelPlate = new Component
        {
            Name = "Steel Plate",
        };
        SteelPlate.Recipe.Add(SteelBlock, 2);
        Component result;
        Inventory.Add(SteelBlock);
        bool canMake = SteelPlate.CanMake(Inventory);
        Console.WriteLine(String.Format("{0} can make it: {1}", Inventory.ItemCount(SteelBlock), canMake));
        Inventory.Add(SteelBlock);
        canMake = SteelPlate.CanMake(Inventory);
        Console.WriteLine(String.Format("{0} can make it: {1}", Inventory.ItemCount(SteelBlock), canMake));
        do
        {
            val = Console.ReadLine();
            Console.WriteLine(val);

        } while (val != "x");
    }
}
