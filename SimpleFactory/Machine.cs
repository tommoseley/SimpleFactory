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
            Plans = new SortedList<string, Plan>(StringComparer.OrdinalIgnoreCase);
            Hopper = new();
            Name = string.Empty;
        }
        public string Name { get; set; }
        public SortedList<string, Plan> Plans { get; set; }
        public Inventory Hopper { get; set; }
        public bool HasPattern (string name)
        {
            return Plans.Count(x => x.Value.Produces == name) > 0;
        }
        public bool CanProduce(string name)
        {
            if (Plans.Count == 0) throw new Exception($"{Name} has no production list");
            if (Plans.Count(x => x.Value.Name.ToLower() == name.ToLower()) > 0)
            {
                return Plans[name].CanMake(Hopper);
            }
            return false;
        }
        public bool Make(string name)
        {
            if (Plans.Count == 0) throw new Exception($"{Name} has no production list");
            if (Plans.Count(x => x.Value.Name.ToLower() == name.ToLower()) > 0)
            {
                return Plans[name].Make(Hopper);
            }
            return false;
        }
        override public string ToString()
        {
            return Name;
        }
        private static SortedList<string, Machine> machines = new SortedList<string, Machine>(StringComparer.InvariantCultureIgnoreCase);
        public static void AddPlan(string machineName, string planName)
        {
            try
            {
                Plan plan = Plan.Get(planName);
                Machine machine = Get(machineName);
                if (machine != null)
                {
                    machine.Plans.Add(planName, plan);
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
        public static void AssignPlans()
        {
            AddPlan("Foundary", "Steel Block");
            AddPlan("Steel Press", "Steel Plate");
            AddPlan("Steel Sheeter", "Steel Sheet");
        }
        public static Machine WhatCanMakePlan(string planName)
        {
            if (machines == null || machines.Count == 0) throw new Exception("No machines defined");
            foreach (Machine machine in machines.Values)
            {
                foreach (Plan plan in machine.Plans.Values)
                {
                    if (string.Compare(plan.Produces, planName, true) == 0)
                        return machine;
                }
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
