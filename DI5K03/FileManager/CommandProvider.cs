using Filemanager.Infrastructure;

namespace Filemanager{
    internal class CommandProvider : ICommandProvider
    {
        public ICommand[] Commands {get;}

        public CommandProvider(){
             {
            Type selftype = typeof(CommandProvider);
            var commands = new List<ICommand>(); 
            foreach (var type in selftype.Assembly.GetTypes())
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
            Commands = [.. commands];
        }
        }

    }
}