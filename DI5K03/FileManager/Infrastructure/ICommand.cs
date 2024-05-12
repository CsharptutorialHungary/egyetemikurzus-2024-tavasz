using Filemanager.Model;

namespace Filemanager.Infrastructure{
    internal interface ICommand{
        string Name {get;}
        void ExecuteAsync(IHost host, string[] args, Cache cache);
    }
}