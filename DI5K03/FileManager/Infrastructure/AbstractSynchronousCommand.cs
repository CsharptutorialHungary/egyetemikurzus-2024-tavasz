using Filemanager.Model;

namespace Filemanager.Infrastructure;
internal abstract class AbstractSynchronousCommand : ICommand
{
    public abstract string Name {get;}

    public Task ExecuteAsync(IHost host, string[] args, Cache cache)
    {
        Execute(host, args, cache);
        return Task.CompletedTask;
    }

    public abstract void Execute(IHost host, string[] args, Cache cache);
}