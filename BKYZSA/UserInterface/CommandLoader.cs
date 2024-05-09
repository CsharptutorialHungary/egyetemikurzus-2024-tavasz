using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BKYZSA.Commands;

namespace BKYZSA.UserInterface
{
    internal class CommandLoader
    {

        public ICommand[] Commands { get; }

        public CommandLoader()
        {
            List<ICommand> commands = new();
            foreach (var type in typeof(CommandLoader).Assembly.GetTypes())
            {
                if (!type.IsAbstract
                    && !type.IsInterface
                    && type.IsAssignableTo(typeof(ICommand)))
                {
                    object? instance = Activator.CreateInstance(type);
                    if (instance is ICommand command)
                    {
                        commands.Add(command);
                    }
                }
            }
            Commands = commands.ToArray();
        }

    }
}
