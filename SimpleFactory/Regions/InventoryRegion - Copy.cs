using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFactory.Regions
{
    public class HeaderRegion : Region
    {
        Inventory inventory;
        public HeaderRegion(int X, int Y, int width, int height, ConsoleColor color) : base(X, Y, width, height, color)
        {
        }
        public override void UpdateText()
        {
            ClearRegion();
            int LineNumber = regionState.Y;
            Console.SetCursorPosition(regionState.X, LineNumber++);
            Console.WriteLine("Stuff you can do:");
            Console.SetCursorPosition(regionState.X, LineNumber++);
            Console.Write("Inv > Inventory Stuff               Mac > Machine Stuff");
            Console.SetCursorPosition(regionState.X, LineNumber++);
        }
    }
}
