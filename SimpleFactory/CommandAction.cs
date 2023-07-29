using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFactory
{
    public struct CommandAction
    {
        public string Command;
        public int Count;
        public string Item;
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
        }
    }
}
