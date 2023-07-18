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
        private List<Machines.Machine> machines;
        public MachinesRegion(int X, int Y, ConsoleColor color, List<Machines.Machine> machines) : base(X, Y, color)
        {
            this.machines = machines;
        }
        public override void UpdateText()
        {
            int LineNumber = regionState.Y;
            Console.WriteLine("Machines:");
            Console.SetCursorPosition(regionState.X, LineNumber++);
            foreach (Machines.Machine machine in machines)
            {
                Console.WriteLine(String.Format("  {0}", machine.Name));
                Console.SetCursorPosition(regionState.X, LineNumber++);
                Console.Write("  Recipes:");
                Console.SetCursorPosition(regionState.X, LineNumber++);
                foreach (Component produced in machine.Produces)
                {
                    string line = String.Format("  {0} - made: {1}", produced.Name, produced.Blueprint.TimesUsed);
                    Console.WriteLine(line);
                    Console.SetCursorPosition(regionState.X, LineNumber++);
                    foreach (Component consumed in produced.Blueprint.Requirements.Keys)
                    {
                        Console.WriteLine(String.Format("    {0} - {1}", consumed.Name, produced.Blueprint.Requirements[consumed]));
                        Console.SetCursorPosition(regionState.X, LineNumber++);
                    }
                }
            }
        }
    }
}
