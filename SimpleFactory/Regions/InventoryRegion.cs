using SimpleFactory.Components;
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
        Components.ComponentCollection inventory;
        public InventoryRegion(int X, int Y, ConsoleColor color, Components.ComponentCollection inventory) : base(X, Y, color)
        {
            this.inventory = inventory;
        }
        public override void UpdateText()
        {
            int LineNumber = regionState.Y;
            Console.SetCursorPosition(regionState.X, LineNumber++);
            Console.Write("Inventory:");
            Console.SetCursorPosition(regionState.X, LineNumber++);
            foreach (Component key in inventory.Keys())
            {
                Console.WriteLine(
                    string.Format("{0} - {1}",
                    key.ToString(),
                    inventory.ItemCount(key)
                    )
                    );
                Console.SetCursorPosition(regionState.X, LineNumber++);
            }
        }
    }
}
