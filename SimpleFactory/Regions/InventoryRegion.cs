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
        ConsoleColor color;
        Components.ComponentCollection inventory;
        public InventoryRegion(int X, int Y, ConsoleColor color, Components.ComponentCollection inventory) : base()
        {
            this.X = X;
            this.Y = Y;
            this.color = color;
            this.inventory = inventory;
        }
        public override void UpdateText()
        {

            int originalLeft = Console.CursorLeft;
            int originalTop = Console.CursorTop;
            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = this.color;

            int LineNumber = this.Y;
            Console.SetCursorPosition(this.X, LineNumber++);
            Console.Write("Inventory:");
            Console.SetCursorPosition(this.X, LineNumber++);
            foreach (Component key in inventory.Keys())
            {
                Console.WriteLine(
                    string.Format("{ 0} - { 1}",
                    key.ToString(),
                    inventory.ItemCount(key)
                    )
                    );
            }
        }


        public override void WriteText(string text)
        {
            throw new NotImplementedException();
        }
    }
}
