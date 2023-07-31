using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Collections;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Runtime.CompilerServices;

namespace SimpleFactory
{

    public class Thing //: IComparable<Thing>
    {
        private static SortedList <string, int> things = new SortedList<string, int>(StringComparer.OrdinalIgnoreCase);
        public static bool Exists (string name)
        {
            return things.ContainsKey(name);
        }
        public static int CostBasis(string name)
        {
            try
            {
                return things[name];
            }
            catch (KeyNotFoundException)
            {
                throw new ThingNotFoundException($"Thing {name} not found");
            }
        }
        public static IList<string> Get()
        {
            return things.Keys;
        }

        private static void Add(string name, int costBasis)
        {
            things.Add(name, costBasis);
        }
        public static void Create()
        {
            Add("Carbon", 25);
            Add("Iron", 50);
            Add("Steel Block", 150);
            Add("Steel Plate", 500);
            Add("Steel Sheet", 600);
        }

    }
    public class ThingNotFoundException : Exception
    {
        public ThingNotFoundException()
        {
        }

        public ThingNotFoundException(string message) : base(message)
        {
        }

        public ThingNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ThingNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

