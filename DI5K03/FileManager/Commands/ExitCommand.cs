using Filemanager.Infrastructure;
using Filemanager.Model;

namespace Filemanager.Commands{
    internal class ExitCommand : ICommand
    {
        public string Name => "exit";

        public void Execute(IHost host, string[] args, Cache cache)
        {
            host.Exit("<< Goodbye >>");
        }
    }
}