using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFactory.CommandHandlers
{
    [CommandMetadata("Buy", "Buys an item from the store.", "Use Buy to buy an item from the store. You can buy multiple items at once.", "buy", "b")]
    public class BuyCommand : InventoryCommand
    {
        //change the values to attributes of the command
        public BuyCommand() : base()
        {
        }

        public override Result Execute()
        {
            Status.Current.Set($"You bought {Count} {Item}.", true);
            Console.WriteLine($"You bought {Count} {Item}.");
            return Result.Success;
        }
    }
}
