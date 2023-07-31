using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFactory
{
    public delegate void OnStatusChangedHandler(object sender, StatusChangeEventArgs status);
    
    public class StatusChangeEventArgs : EventArgs
    {
        public Status Status { get; set; }
        public StatusChangeEventArgs(Status status)
        {
            this.Status = status;   
        }
    }
    public class Status
    {
        public static Status Current
        {
            get
            {
                return instance;
            }
        }
                
        private static Status instance;
        static Status ()
        {
            instance = new Status();
        }

        public event OnStatusChangedHandler? OnStatusChanged;
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
        public bool isSuccessful { get; set; }
        public Status()
        {
            Message = "";
            isSuccessful = false;
            Timestamp = DateTime.Now;
        }
        public void Set (string message, bool isSuccessful)
        {
            Message = message;
            Timestamp = DateTime.Now;
            this.isSuccessful = isSuccessful;
            if (OnStatusChanged != null)
                OnStatusChanged(this, new StatusChangeEventArgs(this));
        }
    }
}
