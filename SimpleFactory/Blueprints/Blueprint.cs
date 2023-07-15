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
            Produced = null;
        }
        public Blueprint(Component produced)
        {
            Produced = produced;
            Requirements = new();
        }
        public Component Produced { get; set; }

        public Dictionary<Component, int> Requirements { get; set; }
        public void AddRequirement(Component component, int count)
        {
            Requirements.Add(component, count);
        }
        public void AddRequirement(string componentName, int count)
        {
            Component component = ComponentFactory.GetComponent(componentName);
            if (component != null)
            {
                Requirements.Add(component, count);
            }
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
                inventory.Add(Produced);
                return true;
            }
            return (false);
        }
    }
}
