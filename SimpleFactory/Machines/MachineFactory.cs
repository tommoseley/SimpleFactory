using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFactory.Machines
{
    public static class MachineFactory
    {
        private static SortedList<string, Machine> machines = new SortedList<string, Machine>(StringComparer.OrdinalIgnoreCase);

        public static Machine GetMachine(string name)
        {
            if (machines.ContainsKey(name))
                return machines[name];
            else
            {
                return null;
            }
        }
        public static Machine AddMachine (string name)
        { 
            Machine machine = new Machine();
            machine.Name = name;
            machines.Add(name, machine);
            return machine;
        }
        public static void CreateMachines()
        {
            AddMachine("Foundary");
            AddMachine("Steel Press");
            AddMachine("Steel Sheeter");
        }
        public static List<Machine> GetMachines()
        {
            return machines.Values.ToList();
        }
    }
}
