using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFactory
{
    [Serializable]
    public class Blueprint
    {
        public Blueprint ()
        {
            Requirements = new List<InventoryItem>();
            Name = "Blueprint";
        }
        public string? Name { get; set; }
        public List<InventoryItem> Requirements { get; set;}

        public bool CanMake(List<InventoryItem> inventory)
        {
            foreach (InventoryItem item in Requirements)
            {
                InventoryItem examined = inventory.FirstOrDefault(x => x.Name == item.Name);
                if (examined == null)
                {
                    return false;
                }
                if (examined.Number < item.Number)
                {
                    return false;
                }

            }
            return true;
        }
        public InventoryItem Make(List<InventoryItem> inventory)
        {
            foreach (InventoryItem item in Requirements)
            {
                InventoryItem examined = inventory.FirstOrDefault(x => x.Name == item.Name);
                if (examined == null)
                {
                    return null;
                }
                examined.Remove (item.Number);  
            }
            return new InventoryItem(Name, 1);
        }
    }
}
