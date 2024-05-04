using DownloadManager.Infrastructure;

namespace DownloadManager.Application
{
    internal class ModeCommand : ICommand
    {
        public string Name => "mode";
        public string Description => "Az alkalmazés a megadott módba lép";
        public Mode[] ValidModes => [];
        public string[] ValidArguments { get; } = Enum.GetNames(typeof(Mode)).Select(name => name.ToLower()).ToArray();

        public void Execute(IHost host, Controller controller, ICommandLoader commandLoader, string[] args)
        {
            if (ValidModes.Length == 0 || ValidModes.Contains(controller.CurrentMode))
            {
                if (args.Length == 1)
                {
                    if (ValidArguments.Contains(args[0]))
                    {
                        controller.CurrentMode = Enum.Parse<Mode>(char.ToUpper(args[0][0]) + args[0][1..]);
                    }
                    else
                    {
                        throw new InvalidOperationException("Ismeretlen argumentum");
                    }
                }
                else
                {
                    throw new InvalidOperationException("Az argumentumok száma nem megfelelő");
                }
            }
            else
            {
                throw new InvalidOperationException("Ebben a módban nem hajtható végre ez a parancs");
            }
        }
    }
}