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
        private SortedList<DateTime, StatusContent> statuses = new SortedList<DateTime, StatusContent>();
        public StatusRegion(int X, int Y, int width, int height, ConsoleColor color, ConsoleColor errorColor, StatusContent status) : base(X, Y, width, height, color)
        {
            ErrorColor = errorColor;
            StatusContent.OnStatusChangedHandler handler = new StatusContent.OnStatusChangedHandler(StatusChangeHandler);
            status.OnStatusChanged += handler;
        }
        public void StatusChangeHandler(object sender, StatusContent status)
        {
            StatusContent newContent = status.Clone() as StatusContent;
            statuses.Add(newContent.Timestamp, newContent);
        }
        public override void UpdateText()
        {
            int LineNumber = regionState.Y;
            Console.SetCursorPosition(regionState.X, LineNumber++);
            foreach (KeyValuePair<DateTime, StatusContent> pair in statuses.Reverse())
            {
                Console.ForegroundColor = pair.Value.isSuccessful ? regionState.Color : ErrorColor;
                Console.WriteLine(string.Format("{0} - {1}", pair.Value.Timestamp.ToString(), pair.Value.Message));
                Console.SetCursorPosition(regionState.X, LineNumber++);
            }
        }
    }
}
