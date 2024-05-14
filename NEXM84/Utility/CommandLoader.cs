using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NEXM84.Application;
using NEXM84.Infrastructure;

namespace NEXM84.Utility
{
    internal class CommandLoader : ICommandProvider
    {

        public ICommand[] commands { get; }

        public CommandLoader()
        {
            Type t = typeof(CommandLoader);
            var commands = new List<ICommand>();
            foreach (var type in t.Assembly.GetTypes())
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
            this.commands = commands.ToArray();
        }
    }
}
