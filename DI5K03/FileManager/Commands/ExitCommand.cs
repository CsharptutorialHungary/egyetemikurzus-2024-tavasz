using Filemanager.Infrastructure;
using Filemanager.Model;

namespace Filemanager.Commands
{
    internal class ExitCommand : AbstractSynchronousCommand
    {
        public override string Name => "exit";

        public override void Execute(IHost host, string[] args, Cache cache)
        {
            host.Exit("<< Goodbye >>");
        }
    }
}