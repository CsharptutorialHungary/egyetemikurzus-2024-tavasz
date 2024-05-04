using DownloadManager.Infrastructure;

namespace DownloadManager.Application
{
    internal class ClearCommand : ICommand
    {
        public string Name => "clear";
        public string Description => "Kitörli a korábbi log-okat";
        public Mode[] ValidModes => [Mode.Logs];
        public string[] ValidArguments => [];

        public void Execute(IHost host, Controller controller, ICommandLoader commandLoader, string[] args)
        {
            if (ValidModes.Contains(controller.CurrentMode))
            {
                if (args.Length == 0)
                {
                    controller.DeleteLogs();
                    host.WriteLine("A log törlésre került");
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