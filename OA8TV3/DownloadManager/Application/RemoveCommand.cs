using DownloadManager.Domain;
using DownloadManager.Infrastructure;

namespace DownloadManager.Application
{
    internal class RemoveCommand : ICommand
    {
        public string Name => "remove";
        public string Description => "Meglévő szabályt töröl\n\tHasználat: remove [szabály_típus] [feltétel]";
        public Mode[] ValidModes => [Mode.Rules];
        public string[] ValidArguments => ["ext", "extension", "min", "max", "pattern"];

        public void Execute(IHost host, Controller controller, ICommandLoader commandLoader, string[] args)
        {
            if (ValidModes.Contains(controller.CurrentMode))
            {
                if (args.Length == 2 || args is ["min"] || args is ["max"])
                {
                    var destinationFolder = new DestinationFolder { FolderName = "", FolderPath = "" };
                    AbstractRule rule = args[0] switch
                    {
                        "ext" or "extension" => new ExtensionRule
                        {
                            Destination = destinationFolder, Extension = args[1]
                        },
                        "min" => new SizeRule { Destination = destinationFolder, ComparisonType = 0, Value = 0 },
                        "max" => new SizeRule { Destination = destinationFolder, ComparisonType = 1, Value = 0 },
                        "pattern" => new PatternRule { Destination = destinationFolder, Pattern = args[1] },
                        _ => throw new InvalidOperationException("Ismeretlen argumentum")
                    };
                    host.WriteLine(controller.DeleteRule(rule)
                        ? "Szabály törölve"
                        : "Ilyen szabály nem található");
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