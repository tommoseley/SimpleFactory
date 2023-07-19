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
    }
}
