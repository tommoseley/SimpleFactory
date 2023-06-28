using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFactory.Machines
{
    [Serializable]
    public class Blueprint
    {
        public Blueprint()
        {
            Requirements = new();
            Name = "Blueprint";
        }
        public Blueprint(string name, Dictionary<string, int> requirements)
        {
            Requirements = requirements;
            Name = name;
        }
        public string? Name { get; set; }
        public Dictionary<string, int> Requirements { get; set; }

        internal bool CanMake(Dictionary<string, int> inventory)
        {
            foreach (string item in Requirements.Keys)
            {
                if (!inventory.ContainsKey(item) || inventory[item] < Requirements[item])
                {
                    return false;
                }

            }
            return true;
        }
        internal bool Make(Dictionary<string, int> inventory)
        {
            if (CanMake(inventory))
            {
                foreach (string item in Requirements.Keys)
                {
                    inventory[item] -= Requirements[item];
                }
                if (!inventory.ContainsKey(Name))
                {
                    inventory.Add(Name, 0);
                }
                inventory[Name] += 1;
                return true;
            }
            return (false);
        }
    }
}
