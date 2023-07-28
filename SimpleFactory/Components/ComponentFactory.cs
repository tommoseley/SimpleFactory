using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
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
        private static void AddComponent(string name, int costBasis)
        {
            Component component = new Component();
            component.Name = name;
            component.CostBasis = costBasis;
            components.Add(name, component);
        }
        public static void CreateComponents()
        {
            AddComponent("Carbon", 25);
            AddComponent("Iron Block", 50);
            AddComponent("Steel Block", 150);
            AddComponent("Steel Plate", 500);
            AddComponent("Steel Sheet", 600);
        }
        public static void SaveComponentsToJSON (string fileName)
        {
            string json = JsonSerializer.Serialize(components);
            System.IO.File.WriteAllText(fileName, json);
        }
        public static void LoadComponentsFromJSON(string fileName)
        {
            string json = System.IO.File.ReadAllText(fileName);
            components = (SortedList<string, Component>)JsonSerializer.Deserialize(json, typeof(SortedList<string, Component>));
            if (components.Count == 0)
            {
                CreateComponents();
            }
        }
        public static void CreateBlueprints()
        {
            var SteelBlock = GetComponent("Steel Block");
            if (SteelBlock != null)
            {
                Blueprints.Blueprint blueprint = SteelBlock.CreateBluePrint();
                blueprint.AddRequirement("Iron Block", 2);
                blueprint.AddRequirement("Carbon", 1);
            }
            var SteelPlate = GetComponent("Steel Plate");
            if (SteelPlate != null)
            {
                Blueprints.Blueprint blueprint = SteelPlate.CreateBluePrint();
                blueprint.AddRequirement("Steel Block", 3);
            }
            var SteelSheet = GetComponent("Steel Sheet");
            if (SteelSheet != null)
            {
                Blueprints.Blueprint blueprint = SteelSheet.CreateBluePrint();
                blueprint.AddRequirement("Steel Plate", 1);
            }
        }

    }
}
