using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFactory.Regions
{
    public class MachinesRegion : Region
    {
        public MachinesRegion(int X, int Y, int width, int height, ConsoleColor color) : base(X, Y, width, height, color)
        {
        }
        public override void UpdateText()
        {
            int LineNumber = regionState.Y;
            foreach (Machine machine in Machine.Get())
            {
                Console.SetCursorPosition(regionState.X, LineNumber++);
                Console.WriteLine($"Machine: {machine.Name}");
                foreach (Blueprint blueprint in machine.Patterns.Values)
                {
                    Console.SetCursorPosition(regionState.X + 2, LineNumber++);
                    string line = String.Format("Recipe: {0}", blueprint.Name);
                    Console.WriteLine($"Machine: {blueprint.Name}");
                //    foreach (Component consumed in produced.Blueprint.Requirements.Keys)
                //    {
                //        Console.SetCursorPosition(regionState.X + 4, LineNumber++);
                //        Console.WriteLine(String.Format("{0} - {1}", consumed.Name, produced.Blueprint.Requirements[consumed]));
                //    }
                }
                LineNumber++;
            }
        }
    }
}
