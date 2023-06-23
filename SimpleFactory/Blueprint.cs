using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFactory
{
    internal class Blueprint
    {
        public Blueprint (string Name)
        {

        }
        public string Name { get; set; }
        public Dictionary<string, int> Recipe = new Dictionary<string, int> ();
        public bool CanMake(Dictionary<string, int> inventory)
        {
            foreach (string key in Recipe.Keys)
            {
                if (inventory.ContainsKey (key) == false || inventory[key] < Recipe[key])
                {
                    return false;
                }

            }
            return true;
        }

    }
}
