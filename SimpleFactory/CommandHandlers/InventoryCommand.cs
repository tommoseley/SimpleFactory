using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFactory.CommandHandlers
{
    public abstract class InventoryCommand : Command
    {
        public InventoryCommand() : base()
        {
        }
        public void SetParameters (string item, int count = 1)
        {
            Item = item;
            Count = count;
        }
        public string Item = string.Empty;
        public int Count = 0;
    }
}
