using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFactory.CommandHandlers
{
    public class CommandFactory
    {
        private CommandFactory()
        {
        }
        public static CommandFactory Instance = new CommandFactory();
        public Command Get (string command)
        {
            List<string> parts = command.Split(' ').ToList<string>();
            string CommandName = parts[0];
            Command returnValue = CommandFactory.Instance.Create(CommandName);
            if (returnValue is InventoryCommand)
            {
                InventoryCommand inventoryCommand = returnValue as InventoryCommand;
                parts.RemoveAt(0);
                int itemCount = 0;
                if (int.TryParse(parts[0], out int count))
                {
                    itemCount = count;
                    parts.RemoveAt(0);
                }
                //convert the parts to a string
                string itemName = parts.Aggregate((first, second) => first + " " + second);
                inventoryCommand.SetParameters(itemName, itemCount);
            }
            return returnValue;
        }
        public Command Create(string command)
        {
            foreach (Type type in GetCommandTypes())
            {
                CommandMetadataAttribute? metadata = type.GetCustomAttribute<CommandMetadataAttribute>();
                if (metadata != null)
                {
                    if (metadata.Name.Equals(command, StringComparison.OrdinalIgnoreCase) ||
                                               metadata.Aliases.Any(alias => alias.Equals(command, StringComparison.OrdinalIgnoreCase)))
                    {
                        return (Command)Activator.CreateInstance(type);
                    }
                }
            }
            return new InvalidCommand();
        }

        // ...

        private IEnumerable<Type> GetCommandTypes()
        {
            Assembly assembly = Assembly.GetExecutingAssembly(); // or the assembly containing your command classes
            return assembly.GetTypes()
                           .Where(type => type.IsSubclassOf(typeof(Command)) &&
                                          type.GetCustomAttribute<CommandMetadataAttribute>() != null);
        }
    }
}
