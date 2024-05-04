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
            ValidArguments = commandLoader.GetCommandsByMode(controller.CurrentMode).Select(cmd => cmd.Name).ToArray();

            if (ValidModes.Length == 0 || ValidModes.Contains(controller.CurrentMode))
            {
                var validCommands = commandLoader.GetCommandsByMode(controller.CurrentMode);
                switch (args.Length)
                {
                    case 0:
                        {
                            host.WriteLine("Jelenleg elérhető parancsok listája:");
                            foreach (ICommand command in validCommands)
                            {
                                host.WriteLine($"{command.Name} - {command.Description}");
                            }
                            break;
                        }
                    case 1:
                        {
                            if (ValidArguments.Contains(args[0]))
                            {
                                foreach (ICommand command in validCommands)
                                {
                                    if (command.Name.Equals(args[0]))
                                    {
                                        host.WriteLine($"{command.Name} - {command.Description}");
                                        host.WriteLine($"Valid arguments: {string.Join(", ", command.ValidArguments)}");
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
            else
            {
                throw new InvalidOperationException("Ebben a módban nem hajtható végre ez a parancs");
            }
        }
    }
}