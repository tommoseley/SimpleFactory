using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SimpleFactory
{
    [Serializable]
    public class Plan
    {
        public Plan()
        {
            Name = string.Empty;
            Produces = string.Empty;
            Requires = new();
        }
        public Plan(string produces, string name) : this()
        {
            try
            {
                Name = name;
                Produces = produces;
            }
            catch (Exception)
            {
                throw new Exception($"Could not find {produces} in the list of things");
            }
        }
        public string Name { get; set; }
        public string Produces { get; set; }
        public Dictionary<string, int> Requires { get; set; }
        public void AddRequirement(string componentName, int count)
        {
            Requires.Add(componentName, count);
        }
        internal bool CanMake(Inventory inventory)
        {
            foreach (string key in Requires.Keys)
            {
                if (!inventory.Items.ContainsKey(key)) return false;

            }
            return true;
        }
        internal bool Make(Inventory inventory)
        {
            if (CanMake(inventory))
            {
                foreach (string key in Requires.Keys)
                {
                    inventory.Remove(key, Requires[key]);
                }
                inventory.Add(Produces, 1);
                return true;
            }
            return false;
        }
        public class Requirement
        {
            public Requirement(string componentName, int count)
            {
                Name = componentName;
                Count = count;
            }
            public string Name { get; set; }
            public int Count { get; set; }
        }
        private static SortedList<string, Plan> contents = new SortedList<string, Plan>(StringComparer.OrdinalIgnoreCase);
        public static void Create()
        {
            Add("Steel Block", "Steel Block", new Requirement("Iron", 5), new Requirement("Carbon", 1));
            Add("Steel Plate", "Steel Plate", new Requirement("Steel Block", 3));
            Add("Steel Sheet", "Steel Sheet", new Requirement("Steel Plate", 1));
        }
        public static Plan Get(string name)
        {
            if (contents.ContainsKey(name))
                return contents[name];
            else
            {
                throw new BlueprintNotFoundException($"Blueprint {name} not found");
            }
        }
        public static void Add(string name, string produced, params Requirement[] requirements)
        {
            Plan blueprint = new(produced, name);
            foreach (Requirement requirement in requirements)
            {
                blueprint.AddRequirement(requirement.Name, requirement.Count);
            }
            contents.Add(name, blueprint);
        }
        override public string ToString()
        {
            return Name;
        }
    }
    public class BlueprintNotFoundException : Exception
    {
        public BlueprintNotFoundException(string message) : base(message)
        {
        }
    }
}
