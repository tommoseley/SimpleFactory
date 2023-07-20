using SimpleFactory.Components;
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
        private SortedList<string, Machines.Machine> machines;
        public MachinesRegion(int X, int Y, int width, int height, ConsoleColor color, SortedList<string, Machines.Machine> machines) : base(X, Y, width, height, color)
        {
            this.machines = machines;
        }
        public override void UpdateText()
        {
            int LineNumber = regionState.Y;
            foreach (Machines.Machine machine in machines.Values)
            {
                Console.SetCursorPosition(regionState.X, LineNumber++);
                Console.WriteLine(String.Format("Machine: {0}", machine.Name));
                foreach (Component produced in machine.Produces)
                {
                    Console.SetCursorPosition(regionState.X + 2, LineNumber++);
                    string line = String.Format("Recipe: {0} - made: {1}", produced.Name, produced.Blueprint.TimesUsed);
                    Console.WriteLine(line);
                    foreach (Component consumed in produced.Blueprint.Requirements.Keys)
                    {
                        Console.SetCursorPosition(regionState.X + 4, LineNumber++);
                        Console.WriteLine(String.Format("{0} - {1}", consumed.Name, produced.Blueprint.Requirements[consumed]));
                    }
                }
                LineNumber++;
            }
        }
    }
}
