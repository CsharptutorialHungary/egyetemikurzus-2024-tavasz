using DownloadManager.Infrastructure;

namespace DownloadManager.Application
{
    internal class HelpCommand : ICommand
    {
        public string Name => "help";
        public string Description => "Információt ad az alkalmazás parancsairól";
        public Mode[] ValidModes => [];
        public string[] ValidArguments { get; private set; } = [];

        public void Execute(IHost host, Controller controller, ICommandLoader commandLoader, string[] args)
        {
            ValidArguments = commandLoader.Commands.Select(cmd => cmd.Name).ToArray();

            var allCommands = commandLoader.Commands;
            var validCommands = commandLoader.GetCommandsByMode(controller.CurrentMode);
            switch (args.Length)
            {
                case 0:
                    {
                        host.WriteLine("Jelenleg elérhető parancsok listája:");
                        foreach (ICommand command in validCommands)
                        {
                            host.WriteLine($"{command.Name} -\t{command.Description}");
                        }
                        break;
                    }
                case 1:
                    {
                        if (ValidArguments.Contains(args[0]))
                        {
                            foreach (ICommand command in allCommands)
                            {
                                if (command.Name.Equals(args[0]))
                                {
                                    host.WriteLine($"{command.Name} -\t{command.Description}");
                                    host.WriteLine(command.ValidModes.Length == 0
                                        ? "\tAz alábbi módokban használható: mind"
                                        : $"\tAz alábbi módokban használható: {string.Join(", ", command.ValidModes)}");
                                    host.WriteLine(command.ValidArguments.Length == 0
                                        ? "\tEnnek a parancsnak nincsenek argumentumai"
                                        : $"\tAz alábbi argumentumok használhatók: {string.Join(", ", command.ValidArguments)}");
                                    break;
                                }
                            }
                            break;
                        }
                        else
                        {
                            throw new InvalidOperationException("Ismeretlen argumentum");
                        }
                    }
                default:
                    {
                        throw new InvalidOperationException("Az argumentumok száma nem megfelelő");
                    }
            }
        }
    }
}