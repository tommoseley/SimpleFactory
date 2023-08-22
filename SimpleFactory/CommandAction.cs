using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SimpleFactory.CommandHandlers;

namespace SimpleFactory
{
    public delegate void CommandEventHandler(object sender, CommandEventArgs e);
    public class CommandEventArgs : EventArgs
    {
        public string Command;
        public int Count;
        public string Item;
        public string RawText;
        public CommandEventArgs(string command, int count, string item, string rawText)
        {
            Command = command;
            Count = count;
            Item = item;
            RawText = rawText;
        }
    }
}
