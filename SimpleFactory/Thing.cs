using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Collections;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace SimpleFactory
{

    public class Thing : IComparable<Thing>
    {
        public Thing()
        {
            Name = string.Empty;
            CostBasis = 0;
        }
        public int CostBasis { get; set; }
        public string Name { get; set; }

        public int CompareTo(Thing? other)
        {
            if (other == null)
                return 1;
            return Name == other.Name ? 0 : 1;
        }
        //[JsonIgnore]
        public override string ToString()
        {
            return Name;
        }
        private static SortedList<string, Thing> things = new SortedList<string, Thing>(StringComparer.OrdinalIgnoreCase);
        public static Thing Get(string name)
        {
            if (things.ContainsKey(name))
                return things[name];
            else
            {
                return null;
            }
        }
        public static IList<Thing> Get()
        {
            return things.Values;
        }
        private static void Add(string name, int costBasis)
        {
            Thing component = new Thing();
            component.Name = name;
            component.CostBasis = costBasis;
            things.Add(name, component);
        }
        public static void Create()
        {
            Add("Carbon", 25);
            Add("Iron", 50);
            Add("Steel Block", 150);
            Add("Steel Plate", 500);
            Add("Steel Sheet", 600);
        }
        public static void SaveToJSON(string fileName)
        {
            string json = JsonSerializer.Serialize(things);
            File.WriteAllText(fileName, json);
        }
        public static void LoadFromJSON(string fileName)
        {
            string json = File.ReadAllText(fileName);
            things = (SortedList<string, Thing>)JsonSerializer.Deserialize(json, typeof(SortedList<string, Thing>));
            if (things.Count == 0)
            {
                Create();
            }
        }
    }
}

