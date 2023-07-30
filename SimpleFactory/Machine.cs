using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SimpleFactory
{
    public class Machine
    {
        public Machine()
        {
            Patterns = new SortedList<string, Blueprint>(StringComparer.OrdinalIgnoreCase);
            Hopper = new();
            Name = string.Empty;
        }
        public string Name { get; set; }
        public SortedList<string, Blueprint> Patterns { get; set; }
        public ThingCollection Hopper { get; set; }
        public bool HasPattern (string name)
        {
            return Patterns.Count(x => x.Value.Produced.Name == name) > 0;
        }
        public bool CanProduce(string name)
        {
            if (Patterns.Count == 0) throw new Exception("Machine has no production list");
            if (Patterns.Count(x => x.Value.Name.ToLower() == name.ToLower()) > 0)
            {
                return Patterns[name].CanMake(Hopper);
            }
            return false;
        }
        public bool Make(string name)
        {
            if (Patterns.Count == 0) throw new Exception("Machine has no production list");
            if (Patterns.Count(x => x.Value.Name.ToLower() == name.ToLower()) > 0)
            {
                return Patterns[name].Make(Hopper);
            }
            return false;
        }
        override public string ToString()
        {
            return Name;
        }
        private static SortedList<string, Machine> machines = new SortedList<string, Machine>(StringComparer.OrdinalIgnoreCase);
        public static void AddPattern(string machineName, string patternName)
        {
            Machine machine = Get(machineName);
            if (machine != null)
            {
                Blueprint pattern = Blueprint.Get(patternName);
                if (pattern != null)
                {
                    machine.Patterns.Add(patternName, pattern);
                }
            }
        }

        public static Machine Get(string name)
        {
            if (machines.ContainsKey(name))
                return machines[name];
            else
            {
                return null;
            }
        }
        public static Machine Add(string name)
        {
            Machine machine = new Machine();
            machine.Name = name;
            machines.Add(name, machine);
            return machine;
        }
        public static void Create()
        {
            Add("Foundary");
            Add("Steel Press");
            Add("Steel Sheeter");
        }
        public static void AssignBlueprints()
        {
            AddPattern("Foundary", "Steel Block");
            AddPattern("Steel Press", "Steel Plate");
            AddPattern("Steel Sheeter", "Steel Sheet");
        }
        public static Machine WhatCanRunRecipe(string name)
        {
            return machines.Values.FirstOrDefault(x => x.Patterns.ContainsKey(name));
        }
        public static List<Machine> Get()
        {
            return machines.Values.ToList();
        }

    }
}
