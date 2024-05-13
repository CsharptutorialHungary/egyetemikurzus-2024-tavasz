using DownloadManager.Infrastructure;

namespace DownloadManager.Application
{
    internal class SortCommand : ICommand
    {
        public string Name => "sort";
        public string Description => "Áthelyezi a letöltés mappa fájljait a megadott szabályok alapján";
        public Mode[] ValidModes => [Mode.Home, Mode.Rules];
        public string[] ValidArguments => [];

        public void Execute(IHost host, Controller controller, ICommandLoader commandLoader, string[] args)
        {
            if (ValidModes.Contains(controller.CurrentMode))
            {
                if (args.Length == 0)
                {
                    int result = controller.SortFiles();
                    host.WriteLine($"{result} darab fájl mozgatva lett");
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