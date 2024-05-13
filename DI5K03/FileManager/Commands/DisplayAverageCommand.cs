using Filemanager.Infrastructure;
using Filemanager.Model;

namespace Filemanager.Commands{
    internal class DisplayAverageCommand : ICommand
    {
        public string Name => "avg-extensions";

        public Task ExecuteAsync(IHost host, string[] args, Cache cache)
        {
            if(cache.Stored_folderdefs.Count == 0){
                return Task.Factory.StartNew(()=>host.WriteLine("There are no Folder definitions for this directory"));
            }
            double average = cache.Stored_folderdefs.Average(folderdef => folderdef.Types.Length);
            return Task.Factory.StartNew(()=>host.WriteLine("The average number of extensions in a folder definition is "+average));
        }
    }
}