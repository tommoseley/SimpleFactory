using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SimpleFactory.CommandAction;

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
    public class CommandAction
    {
        public event CommandEventHandler OnCommand;
        public string Command;
        private int Count;
        private string Item;
        public void Parse(string command)
        {
            string[] parts = command.Split(' ');
            if (parts.Length > 1)
            {
                int startIndex = 1;
                if (int.TryParse(parts[1], out int count))
                {
                    startIndex = 2;
                }
                else
                {
                    count = 1;
                }
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = startIndex; i < parts.Length; i++)
                {
                    stringBuilder.Append(parts[i]);
                    if (i < parts.Length - 1)
                        stringBuilder.Append(" ");
                }
                Command = parts[0];
                Count = count;
                Item = stringBuilder.ToString().Trim();
            }
            else
            {
                Command = parts[0];
                Count = 1;
                Item = "";
            }
            if (OnCommand != null)
            {
                OnCommand(this, new CommandEventArgs(Command, Count, Item, command));
            }
        }

    }
}
