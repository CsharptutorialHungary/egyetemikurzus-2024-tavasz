using Filemanager.Infrastructure;

namespace Filemanager.Commands{
    internal class ExitCommand : ICommand
    {
        public string Name => "exit";

        public void Execute(IHost host, string[] args)
        {
            host.Exit("<< Goodbye >>");
        }
    }
}