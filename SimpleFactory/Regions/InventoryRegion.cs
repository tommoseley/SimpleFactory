using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFactory.Regions
{
    public class InventoryRegion : Region
    {
        public Inventory Parts;
        public InventoryRegion()
        {
        }
        public override void UpdateText()
        {
            int LineNumber = Y;
            Console.SetCursorPosition(X, LineNumber++);
            Console.Write("Inventory:");
            Console.SetCursorPosition(X, LineNumber++);
            foreach (string key in Parts.Items.Keys)
            {
                string line = $"{key} - {Parts.Items[key].Quantity}";
                Console.WriteLine(line);
                Console.SetCursorPosition(X, LineNumber++);
            }
        }
    }
}
