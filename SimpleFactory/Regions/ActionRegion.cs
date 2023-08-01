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
        public ActionRegion(int X, int Y, int width, int height, bool isVisible, ConsoleColor color) : base(X, Y, width, height, isVisible, color)
        {
        }
        public override void UpdateText()
        {
            for (int line = this.regionState.Y; line < regionState.Height; line++)
            {
                Console.CursorTop = line;
                Console.CursorLeft = 0;
                Console.Write(new string(' ', this.regionState.Width / 2 - 2));
            }
            Console.CursorLeft = regionState.X;
            Console.CursorTop = regionState.X;
            Console.WriteLine("Enter a command: buy <count> <item>");
            Console.WriteLine("                 make <recipe>");
            Console.WriteLine("                 exit");
            Console.Write(">");
            Console.CursorLeft = 2;
        }
    }
}
