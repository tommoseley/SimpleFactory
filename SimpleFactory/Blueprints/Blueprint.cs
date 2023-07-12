using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleFactory.Components;
namespace SimpleFactory.Blueprints
{
    [Serializable]
    public class Blueprint
    {
        public Blueprint()
        {
            Requirements = new();
            Name = "Blueprint";
        }
        public Blueprint(string name, Dictionary<Component, int> requirements)
        {
            Requirements = requirements;
            Name = name;
        }
        public string Name { get; set; }
        public Dictionary<Component, int> Requirements { get; set; }
        public void AddComponent (Component component, int count)
        {
            Requirements.Add(component, count);
        }
        internal bool CanMake(ComponentCollection inventory)
        {
            foreach (Component key in Requirements.Keys)
            {
                if (!inventory.Has(key, Requirements[key])) return false;
  
            }
            return true;
        }
        internal bool Make(ComponentCollection inventory)
        {
            if (CanMake(inventory))
            {
                foreach (Component key in Requirements.Keys)
                {
                    inventory.Remove(key, Requirements[key]); 
                }
             //   inventory.Add(Name);
                return true;
            }
            return (false);
        }
    }
}
