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
        public MachinesRegion()
        {
        }
        public override void UpdateText()
        {
            int LineNumber = Y;
            foreach (Machine machine in Machine.Get())
            {
                Console.SetCursorPosition(X, LineNumber++);
                Console.WriteLine($"Machine: {machine.Name}");
                foreach (Plan plan in machine.Plans.Values)
                {
                    Console.SetCursorPosition(X + 2, LineNumber++);
                    Console.WriteLine($"Plan: {plan.Name}");
                    Console.SetCursorPosition(X + 4, LineNumber++);
                    Console.WriteLine($"Parts:");
                    foreach (string part in plan.Parts.Keys)
                    {
                        Console.SetCursorPosition(X + 4, LineNumber++);
                        Console.WriteLine($"{part} - {plan.Parts[part]} - to make: {plan.TimeToMake}");
                    }
                }
                LineNumber++;
            }
        }
    }
}
