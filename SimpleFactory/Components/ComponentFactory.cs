using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFactory.Components
{
    public static class ComponentFactory
    {
        // create a sorted list of Components, sorted by the component name

        private static SortedList<string, Component> components = new SortedList<string, Component>();
        public static Component GetComponent(string name)
        {
            if (components.ContainsKey(name))
                return components[name];
            else
            {
                Component component = new Component();
                component.Name = name;
                components.Add(name, component);
                return component;
            }
        }
        public static void CreateComponents()
        {
            GetComponent("Steel Plate");
            GetComponent("Steel Block");
            GetComponent("Carbon");
            GetComponent("Steel Sheet");
            GetComponent("Iron Block");
        }
    }
}
