using DownloadManager.Infrastructure;

namespace DownloadManager.UserInterface
{
    internal class ReflectionCommandLoader : ICommandLoader
    {
        public ICommand[] Commands { get; }

        public ReflectionCommandLoader()
        {
            var typeOfClass = typeof(ReflectionCommandLoader);
            var commands = new List<ICommand>();
            foreach (var type in typeOfClass.Assembly.GetTypes())
            {
                if (!type.IsAbstract && !type.IsInterface && type.IsAssignableTo(typeof(ICommand)))
                {
                    var instance = Activator.CreateInstance(type);
                    if (instance is ICommand command)
                    {
                        commands.Add(command);
                    }
                }
            }
            Commands = commands.ToArray();
        }

        public ICommand[] GetCommandsByMode(Mode mode)
        {
            var filteredCommands = Commands
                .OrderBy(command => command.Description.Length)
                .Select(command => command)
                .Where(command => command.ValidModes.Length == 0 || command.ValidModes.Contains(mode));
            return filteredCommands.ToArray();
        }
    }
}