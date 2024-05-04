using DownloadManager.Infrastructure;

namespace DownloadManager.Application
{
    internal class ModeCommand : ICommand
    {
        public string Name => "mode";
        public string Description => "Az alkalmazés a megadott módba lép";
        public Mode[] ValidModes => [];
        public int[] ValidArgNums => [1];
        public string[] ValidArguments { get; } = Enum.GetNames(typeof(Mode));

        public void Execute(IHost host, Controller controller, Mode currentMode, string[] args)
        {
            if (ValidModes.Length == 0 || ValidModes.Contains(currentMode))
            {
                if (ValidArgNums.Contains(args.Length))
                {
                    if (ValidArguments.Contains(args[0]))
                    {
                        controller.CurrentMode = Enum.Parse<Mode>(args[0]);
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