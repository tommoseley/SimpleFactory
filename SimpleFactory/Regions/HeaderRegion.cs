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
        public HeaderRegion()
        {
        }
        public override void UpdateText()
        {
            int LineNumber = Y;
            Console.SetCursorPosition(X, LineNumber++);
            Console.WriteLine("Stuff you can do:");
            Console.SetCursorPosition(X, LineNumber++);
            Console.Write("Inv > Inventory Stuff               Mac > Machine Stuff");
            Console.SetCursorPosition(X, LineNumber++);
        }
    }
}
