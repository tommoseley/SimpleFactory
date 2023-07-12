using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFactory.Regions
{
    public abstract class Region
    {
        public int  X { get; set; }
        public int Y { get; set; }
        public ConsoleColor Color { get; set; }
        public abstract void WriteText(string text); 
        public abstract void UpdateText();
    }
}