using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFactory
{
    internal class Blueprint
    {
        public Blueprint ()
        {
        }
        public string? Name { get; set; }
        public Dictionary<Component, int> Recipe = new Dictionary<Component, int> ();
 
    }
}
