using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFactory.CommandHandlers
{
    [CommandMetadata("invalid", "Invalid command", "invalid", "invalid")]
    public class InvalidCommand : Command
    {
        public InvalidCommand() : base()
        {
        }

        public override Result Execute()
        {
            return Result.Invalid;
        }
    }
}
