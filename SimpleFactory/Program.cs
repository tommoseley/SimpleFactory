using System;
using System.Collections;
using SimpleFactory;
using SimpleFactory.Components;
using SimpleFactory.Blueprints;
public static class Runner
{
    public static void Main(String[] Args)
    {
        // See https://aka.ms/new-console-template for more information
        Console.WriteLine("Hello, World!");
        string? val = string.Empty;
        Dictionary<string, int> Inventory = new Dictionary<string, int>();
        List<Blueprint> Blueprints = new List<Blueprint>();
        do
        {
            val = Console.ReadLine();
            Console.WriteLine(val);

        } while (val != "x");
    }
}
