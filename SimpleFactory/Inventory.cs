using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFactory
{
    public class Inventory
    {
        public Inventory()
        {
            Items = new SortedDictionary<string, InventoryDetail>(StringComparer.InvariantCultureIgnoreCase);
            Name = string.Empty;
        }
        public string Name { get; set; }
        public SortedDictionary<string, InventoryDetail> Items { get; set; }
        public void Add(string name, int priceBasis, int quantity)
        {
            if (!Items.ContainsKey(name))
                Items.Add(name, new InventoryDetail () { Quantity = quantity, CostBasic = priceBasis });
        }
        public void Add(string name, int quantity)
        {
            if (Items.ContainsKey(name))
            {
                InventoryDetail item = new InventoryDetail()
                { CostBasic = Items[name].CostBasic, Quantity = Items[name].Quantity + quantity };
                Items[name] = item;
            }
        }
        public void Remove (string name, int quantity)
        {
            if (Items.ContainsKey(name))
            {
                InventoryDetail item = new InventoryDetail()
                { CostBasic = Items[name].CostBasic, Quantity = Items[name].Quantity - quantity };
                Items[name] = item;
                if (Items[name].Quantity <= 0)
                    throw new Exception($"Inventory of {name} <0");
            }
        }
        public void Create ()
        {
            Add("Carbon", 25, 0);
            Add("Iron", 50, 0);
            Add("Steel Block", 150, 0);
            Add("Steel Plate", 500, 0);
            Add("Steel Sheet", 600, 0);
        }
        public struct InventoryDetail
        {
            public int Quantity {get; set; }
            public int CostBasic { get; set; }
        }
    }
}
