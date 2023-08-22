using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFactory.CommandHandlers
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    sealed class CommandMetadataAttribute : Attribute
    {
        public string Name { get; }
        public string Description { get; }
        public string Usage { get; }
        public string[] Aliases { get; }

        public CommandMetadataAttribute(string name, string description, string usage, params string[] aliases)
        {
            Name = name;
            Description = description;
            Usage = usage;
            Aliases = aliases;
        }
    }
}
