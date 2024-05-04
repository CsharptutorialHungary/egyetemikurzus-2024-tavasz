using DownloadManager.Infrastructure;

namespace DownloadManager.Application
{
    internal class ListCommand : ICommand
    {
        public string Name => "list";
        public string Description => "Szabályokat vagy log-okat listáz";
        public Mode[] ValidModes => [Mode.Logs, Mode.Rules];
        public string[] ValidArguments => ["filter", "search"];

        public void Execute(IHost host, Controller controller, ICommandLoader commandLoader, string[] args)
        {
            switch (controller.CurrentMode)
            {
                case Mode.Logs:
                    {
                        switch (args.Length)
                        {
                            case 0:
                                {
                                    foreach (string log in controller.SearchLogs())
                                    {
                                        host.WriteLine(log);
                                    }
                                    break;
                                }
                            case 2:
                                {
                                    if (ValidArguments.Contains(args[0]))
                                    {
                                        foreach (string log in controller.SearchLogs(args[1]))
                                        {
                                            host.WriteLine(log);
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
                        break;
                    }
                case Mode.Rules:
                    {
                        host.WriteLine("mmm");
                        break;
                    }
                default:
                    {
                        throw new InvalidOperationException("Ebben a módban nem hajtható végre ez a parancs");
                    }
            }
        }
    }
}