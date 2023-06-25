using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFactory
{
    [Serializable]
    public class InventoryItem
    {
        public InventoryItem() { }
        public InventoryItem(string name) : this(name, 0)
        {

        }
        public InventoryItem(string name, int number)
        {
            Name = name;
            Number = number;
        }

        public string Name { get; set; }
        public int Number { get; set; } 
        public int Add (int added)
        {
            Number += added;
            return Number;
        }
        public int Remove (int removed)
        {
            Number -= removed;
            if (Number < 0)
                Number = 0;
            return Number;
        }
    }
}
