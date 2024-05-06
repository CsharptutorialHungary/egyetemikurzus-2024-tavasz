using DownloadManager.Domain;
using DownloadManager.Infrastructure;

namespace DownloadManager.Application
{
    internal class AddCommand : ICommand
    {
        public string Name => "add";

        public string Description =>
            "Új szabályt hoz létre a megadott típussal\n\tHasználat: add [szabály_típus] [feltétel] [célmappa]";

        public Mode[] ValidModes => [Mode.Rules];
        public string[] ValidArguments => ["ext", "extension", "min", "max", "pattern"];

        public void Execute(IHost host, Controller controller, ICommandLoader commandLoader, string[] args)
        {
            if (ValidModes.Contains(controller.CurrentMode))
            {
                if (args.Length == 3)
                {
                    if (controller.ValidPath(args[2]))
                    {
                        var destinationFolder = new DestinationFolder
                        {
                            FolderName = controller.GetFolderName(args[2]), FolderPath = args[2]
                        };
                        AbstractRule rule;
                        switch (args[0])
                        {
                            case "ext":
                            case "extension":
                                {
                                    rule = new ExtensionRule { Destination = destinationFolder, Extension = args[1] };
                                    break;
                                }
                            case "min":
                                {
                                    if (int.TryParse(args[1], out int value))
                                    {
                                        rule = new SizeRule
                                        {
                                            Destination = destinationFolder, ComparisonType = 0, Value = value
                                        };
                                        break;
                                    }
                                    else
                                    {
                                        throw new InvalidOperationException("A fájlméretnek számnak kell lennie");
                                    }
                                }
                            case "max":
                                {
                                    if (int.TryParse(args[1], out int value))
                                    {
                                        rule = new SizeRule
                                        {
                                            Destination = destinationFolder, ComparisonType = 1, Value = value
                                        };
                                        break;
                                    }
                                    else
                                    {
                                        throw new InvalidOperationException("A fájlméretnek számnak kell lennie");
                                    }
                                }
                            case "pattern":
                                {
                                    rule = new PatternRule { Destination = destinationFolder, Pattern = args[1] };
                                    break;
                                }
                            default:
                                {
                                    throw new InvalidOperationException("Ismeretlen argumentum");
                                }
                        }
                        host.WriteLine(controller.AddRule(rule)
                            ? "Új szabály hozzáadva"
                            : "Már van ilyen típusú szabály definiálva");
                    }
                    else
                    {
                        throw new InvalidOperationException("A megadott mappa nem létezik");
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