using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFactory
{
    public class Status
    {
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
        public bool isSuccessful { get; set; }
        public Status()
        {
            Message = "";
            isSuccessful = false;
            Timestamp = DateTime.Now;
        }

    }
}
