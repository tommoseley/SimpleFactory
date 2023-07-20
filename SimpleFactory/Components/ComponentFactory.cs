﻿using System;
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
        public static void SaveComponentsToJSON (string fileName)
        {
            string json = JsonSerializer.Serialize(components);
            System.IO.File.WriteAllText(fileName, json);
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
