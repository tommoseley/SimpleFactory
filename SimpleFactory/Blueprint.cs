using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SimpleFactory
{
    [Serializable]
    public class Blueprint
    {
        public Blueprint()
        {
            Requirements = new();
            Produced = null;
            Name = string.Empty;
        }
        public Blueprint(string produced, string name) : this()
        {
            Produced = Thing.Get(produced);
            Name = name;
        }
        public string Name { get; set; }
        public Thing Produced { get; set; }
        public Dictionary<Thing, int> Requirements { get; set; }
        public void AddRequirement(Thing component, int count)
        {
            Requirements.Add(component, count);
        }
        public void AddRequirement(string componentName, int count)
        {
            Thing component = Thing.Get(componentName);
            if (component != null)
            {
                Requirements.Add(component, count);
            }
        }
        internal bool CanMake(ThingCollection inventory)
        {
            foreach (Thing key in Requirements.Keys)
            {
                if (!inventory.Has(key, Requirements[key])) return false;

            }
            return true;
        }
        internal bool Make(ThingCollection inventory)
        {
            if (CanMake(inventory))
            {
                foreach (Thing key in Requirements.Keys)
                {
                    inventory.Remove(key, Requirements[key]);
                }
                inventory.Add(Produced);
                return true;
            }
            return false;
        }
        public class Requirement
        {
            public Requirement(string componentName, int count)
            {
                Component = Thing.Get(componentName);
                Count = count;
            }
            public Thing Component { get; set; }
            public int Count { get; set; }
        }
        private static SortedList<string, Blueprint> contents = new SortedList<string, Blueprint>(StringComparer.OrdinalIgnoreCase);
        public static void Create()
        {
            Add("Steel Block", "Steel Block", new Requirement("Iron", 5), new Requirement("Carbon", 1));
            Add("Steel Plate", "Steel Plate", new Requirement("Steel Block", 3));
            Add("Steel Sheet", "Steel Sheet", new Requirement("Steel Plate", 1));
        }
        public static Blueprint Get(string name)
        {
            if (contents.ContainsKey(name))
                return contents[name];
            else
            {
                return null;
            }
        }
        public static void Add(string name, string produced, params Requirement[] requirements)
        {
            Blueprint blueprint = new(produced, name);
            foreach (Requirement requirement in requirements)
            {
                blueprint.AddRequirement(requirement.Component, requirement.Count);
            }
            contents.Add(name, blueprint);
        }

    }
}
