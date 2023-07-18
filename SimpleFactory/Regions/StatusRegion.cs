using SimpleFactory.Components;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFactory.Regions
{
    public class StatusRegion:Region
    {
        public struct Status
        {
            public string Message;
            public DateTime Timestamp;
            public bool isSuccessful;
        }
        // Event Handler to be used by clients to listen for status updates


        private List<string> statuses;
        public StatusRegion(int X, int Y, ConsoleColor color, List<Machines.Machine> machines) : base(X, Y, color)
        {
        }
        public override void UpdateText()
        {
            int LineNumber = regionState.Y;
            Console.SetCursorPosition(regionState.X, LineNumber++);
        }
    }
}
