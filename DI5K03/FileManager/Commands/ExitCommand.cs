using Filemanager.Infrastructure;
using Filemanager.Model;

namespace Filemanager.Commands
{
    internal class ExitCommand : ICommand
    {
        public string Name => "exit";

        public async Task ExecuteAsync(IHost host, string[] args, Cache cache)
        {
            await Task.Factory.StartNew(()=>host.Exit("<< Goodbye >>"));
        }
    }
}