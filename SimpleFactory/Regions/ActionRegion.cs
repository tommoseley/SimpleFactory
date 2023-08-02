using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleFactory;
namespace SimpleFactory.Regions
{
    public class ActionRegion:Region
    {
        public ActionRegion()
        {
        }
        public override void UpdateText()
        {
            for (int line = this.Y; line < Height; line++)
            {
                Console.CursorTop = line;
                Console.CursorLeft = 0;
                Console.Write(new string(' ', this.Width / 2 - 2));
            }
            Console.CursorLeft = X;
            Console.CursorTop = X;
            Console.WriteLine("Enter a command: buy <count> <item>");
            Console.WriteLine("                 make <recipe>");
            Console.WriteLine("                 exit");
            Console.Write(">");
            Console.CursorLeft = 2;
        }
    }
}
