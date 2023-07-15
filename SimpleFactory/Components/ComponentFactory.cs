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

        private static SortedList<string, Component> components = new SortedList<string, Component>(StringComparer.OrdinalIgnoreCase);
        public static Component GetComponent(string name)
        {
            if (components.ContainsKey(name))
                return components[name];
            else
            {
                return null;
            }
        }
        private static void AddComponent(string name)
        {
            Component component = new Component();
            component.Name = name;
            components.Add(name, component);
        }
        public static void CreateComponents()
        {
            AddComponent("Steel Plate");
            AddComponent("Steel Block");
            AddComponent("Carbon");
            AddComponent("Steel Sheet");
            AddComponent("Iron Block");
        }
        public static void CreateBlueprints()
        {
            var SteelPlate = GetComponent("Steel Plate");
            if (SteelPlate != null)
            {
                Blueprints.Blueprint blueprint = SteelPlate.CreateBluePrint();
                blueprint.AddRequirement("Steel Block", 2);
                blueprint.AddRequirement("Carbon", 1);
            }
        }

    }
}
