using Filemanager.Model;

namespace Filemanager.Infrastructure{
    internal interface ICommand{
        string Name {get;}
        void Execute(IHost host, string[] args, Cache cache);
    }
}