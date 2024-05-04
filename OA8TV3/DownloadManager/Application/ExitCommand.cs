using System.Reflection;

using DownloadManager.Infrastructure;

using InvalidOperationException = System.InvalidOperationException;

namespace DownloadManager.Application
{
    internal class ExitCommand : ICommand
    {
        public string Name => "exit";
        public string Description => "Az alkalmazás befejezi a működését";
        public Mode[] ValidModes => [];
        public string[] ValidArguments => [];

        public void Execute(IHost host, Controller controller, ICommandLoader commandLoader, string[] args)
        {
            if (ValidModes.Length == 0 || ValidModes.Contains(controller.CurrentMode))
            {
                if (args.Length == 0)
                {
                    host.Exit();
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