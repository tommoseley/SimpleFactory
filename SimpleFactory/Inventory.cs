using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFactory
{
    internal class Inventory
    {
        internal Inventory()
        {
            Items = new();
        }
        public Dictionary<string, int> Items { get; set; }
        public void Add(string name, int quantity)
        {
            if (!Items.ContainsKey(name))
                Items.Add(name, quantity);
            else
                Items[name] += quantity;
        }
        public void Remove (string name, int quantity)
        {
            if (Items.ContainsKey(name))
            {
                Items[name] -= quantity;
                if (Items[name] <= 0)
                    Items.Remove(name);
            }
        }
    }
}
