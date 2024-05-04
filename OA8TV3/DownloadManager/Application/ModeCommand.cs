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
                    string newMode = char.ToUpper(args[0][0]) + args[0][1..];
                    if (ValidArguments.Contains(newMode))
                    {
                        controller.CurrentMode = Enum.Parse<Mode>(newMode);
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