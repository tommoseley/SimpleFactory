using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFactory.Machines
{
    internal class Machine
    {
        public Machine() 
        {
            Blueprints = new();
        }
        public string Name { get; set; }
        public Dictionary<string, Blueprint> Blueprints { get; set; }
        public bool CanMake (string BlueprintName, Dictionary<string, int> inventory)
        {
            if (Blueprints.ContainsKey(BlueprintName))
            {
                return Blueprints[BlueprintName].CanMake(inventory);
            }
            return false;
        }
        public bool Make (string BlueprintName, Dictionary<string, int> inventory)
        {
            if (Blueprints.ContainsKey(BlueprintName))
            {
                return Blueprints[BlueprintName].Make(inventory);
            }
            return false;
        }
    }
}
