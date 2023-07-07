using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFactory
{
    internal class Workflow
    {
        public Workflow()
        {
            Machines = new();
        }
        public Dictionary<string, Machines.Machine> Machines { get; set; }
        public bool CanMake(string MachineName, string BlueprintName)
        {
            if (Machines.ContainsKey(MachineName))
            {
                return Machines[MachineName].CanMake(BlueprintName);
            }
            return false;
        }
        public bool Make(string MachineName, string BlueprintName)
        {
            if (Machines.ContainsKey(MachineName))
            {
                return Machines[MachineName].Make(BlueprintName);
            }
            return false;
        }
    }
}
