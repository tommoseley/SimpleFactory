using SimpleFactory.Components;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleFactory.Status;
namespace SimpleFactory.Regions
{
    public class StatusRegion:Region
    {
        public ConsoleColor ErrorColor { get; set; }
        private List<StatusContent> statuses = new List<StatusContent>();
        public StatusRegion(int X, int Y, int width, int height, ConsoleColor color, ConsoleColor errorColor, StatusContent status) : base(X, Y, width, height, color)
        {
            ErrorColor = errorColor;
            StatusContent.OnStatusChangedHandler handler = new StatusContent.OnStatusChangedHandler(StatusChangeHandler);
            status.OnStatusChanged += handler;
        }
        public void StatusChangeHandler(object sender, StatusContent status)
        {
            statuses.Add(status.Clone() as StatusContent);
        }
        public override void UpdateText()
        {
            int LineNumber = regionState.Y;
            Console.SetCursorPosition(regionState.X, LineNumber++);
            foreach (StatusContent status in statuses)
            {
                Console.ForegroundColor = status.isSuccessful ? regionState.Color : ErrorColor;
                Console.WriteLine(string.Format("{0} - {1}", status.Timestamp.ToString(), status.Message));
                Console.SetCursorPosition(regionState.X, LineNumber++);
            }
        }
    }
}
