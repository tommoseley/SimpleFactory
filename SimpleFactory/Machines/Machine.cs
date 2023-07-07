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
            Hopper = new();
            Name = string.Empty;
        }
        public string Name { get; set; }
        public Dictionary<string, Blueprint> Blueprints { get; set; }
        public ComponentCollection Hopper { get; set; }  
        public bool CanMake (string BlueprintName)
        {
            if (Blueprints.ContainsKey(BlueprintName))
            {
                return Blueprints[BlueprintName].CanMake(Hopper);
            }
            return false;
        }
        public bool Make (string BlueprintName)
        {
            if (Blueprints.ContainsKey(BlueprintName))
            {
                return Blueprints[BlueprintName].Make(Hopper);
            }
            return false;
        }
    }
}
