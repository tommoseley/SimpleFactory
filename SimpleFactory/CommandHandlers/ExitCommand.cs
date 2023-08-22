using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFactory.CommandHandlers
{
    [CommandMetadata("Exit", "Exits the program.", "Use Exit to leave the program. Don't worry, your status will be saved.", "exit", "quit", "q", "x")]
    public class ExitCommand : Command
    {
        public ExitCommand() : base()
        {
        }

        public override Result Execute()
        {
            return Result.End;
        }
    }
}
