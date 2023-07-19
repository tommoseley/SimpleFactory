using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFactory.Status
{
    public class StatusContent : ICloneable
    {
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
        public bool isSuccessful { get; set; }
        public StatusContent()
        {
            Message = "";
            isSuccessful = false;
            Timestamp = DateTime.Now;
        }
        public delegate void OnStatusChangedHandler(object sender, StatusContent status);

        public event OnStatusChangedHandler OnStatusChanged;
        public void SetStatus(string message, bool isSuccessful)
        {
            Message = message;
            this.isSuccessful = isSuccessful;
            Timestamp = DateTime.Now;
            if (OnStatusChanged != null)
                OnStatusChanged(this, this);
        }
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
