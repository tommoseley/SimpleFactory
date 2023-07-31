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
        public Inventory Hopper { get; set; }
        public bool HasPattern (string name)
        {
            return Patterns.Count(x => x.Value.Produced == name) > 0;
        }
        public bool CanProduce(string name)
        {
            if (Patterns.Count == 0) throw new Exception($"{Name} has no production list");
            if (Patterns.Count(x => x.Value.Name.ToLower() == name.ToLower()) > 0)
            {
                return Patterns[name].CanMake(Hopper);
            }
            return false;
        }
        public bool Make(string name)
        {
            if (Patterns.Count == 0) throw new Exception($"{Name} has no production list");
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
        private static SortedList<string, Machine> machines = new SortedList<string, Machine>(StringComparer.InvariantCultureIgnoreCase);
        public static void AddPattern(string machineName, string patternName)
        {
            try
            {
                Blueprint pattern = Blueprint.Get(patternName);
                Machine machine = Get(machineName);
                if (machine != null)
                {
                    machine.Patterns.Add(patternName, pattern);
                }
            }
            catch (MachineNotFoundException e)
            {
                Status.Current.Set(e.Message, false);
            }
            catch (BlueprintNotFoundException e)
            {
                Status.Current.Set(e.Message, false);
            }
        }

        public static Machine Get(string name)
        {
            try
            {
                return machines[name];
            }
            catch (KeyNotFoundException)
            {
                throw new MachineNotFoundException($"Machine {name} not found");
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
            AddPattern("Foundarsssy", "Iron Block");
            AddPattern("Foundary", "Steel Block");
            AddPattern("Steel Press", "Steel Plate");
            AddPattern("Steel Sheeter", "Steel Sheet");
        }
        public static Machine WhatCanRunRecipe(string name)
        {
            if (machines == null || machines.Count == 0) throw new Exception("No machines defined");
            foreach (Machine machine in machines.Values)
            {
                if (machine.Patterns.Count(x => string.Compare(x.Value.Produced, name, true) != 0))
                    return machine;
            }
            return null;
        }
        public static List<Machine> Get()
        {
            return machines.Values.ToList();
        }
    }
    public class MachineNotFoundException : Exception
    {
        public MachineNotFoundException(string message) : base(message)
        {
        }
    }
}
