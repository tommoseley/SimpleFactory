using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFactory.CommandHandlers
{
    public abstract class Command
    {
        public enum Result
        {
            Success,
            Failure,
            Invalid,
            End
        }
        public string Name = string.Empty;
        public string Description = string.Empty;
        public string Usage = string.Empty;
        public List<string> Aliases = new List<string>();
        public Command()
        {
        }
        abstract public Result Execute();
    }
}
    