﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SimpleFactory.Regions
{
    public class StatusRegion:Region
    {
        public ConsoleColor ErrorColor { get; set; }
        private SortedList<DateTime, StatusRecord> statuses = new SortedList<DateTime, StatusRecord>();
        public StatusRegion()
        { 
            OnStatusChangedHandler handler = new OnStatusChangedHandler(StatusChangeHandler);
            Status.Current.OnStatusChanged += handler;
        }
        public void StatusChangeHandler(object sender, StatusChangeEventArgs status)
        {
            StatusRecord record = new StatusRecord (status.Status); ;
            statuses.Add(record.Timestamp, record);
        }
        public override void UpdateText()
        {
            int LineNumber = Y;
            Console.SetCursorPosition(X, LineNumber++);
            foreach (KeyValuePair<DateTime, StatusRecord> pair in statuses.Reverse())
            {
                Console.ForegroundColor = pair.Value.isSuccessful ? Color : ErrorColor;
                Console.WriteLine(string.Format("{0:} - {1}", pair.Value.Timestamp.ToString(), pair.Value.Message));
                Console.SetCursorPosition(X, LineNumber++);
            }
        }
        internal class StatusRecord
        {
            internal DateTime Timestamp { get; set; }
            internal string Message { get; set; }
            internal bool isSuccessful { get; set; }
            internal StatusRecord(Status status)
            {
                Timestamp = status.Timestamp;
                Message = status.Message;
                this.isSuccessful = status.isSuccessful;
            }
        }
    }
}
