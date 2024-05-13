using Filemanager.Infrastructure;
using Filemanager.Model;

namespace Filemanager.Commands{
    internal class DisplayTopFiveCommand : ICommand
    {
        public string Name => "most-extensions";

        public Task ExecuteAsync(IHost host, string[] args, Cache cache)
        {
            if(cache.Stored_folderdefs.Count == 0){
                return Task.Factory.StartNew(()=>host.WriteLine("There are no Folder definitions for this directory"));
            }
            IEnumerable<FolderDef> most_used_folder_defs = cache.Stored_folderdefs.OrderByDescending(folder_def => folder_def.Types.Length).Take(5);
            
            host.WriteLine("The top "+most_used_folder_defs.Count()+" folder definitions with the most extensions are:");
            foreach (FolderDef folder_def in most_used_folder_defs){
                host.WriteLine("-    "+folder_def.Name+": "+folder_def.Types.Length);
            }

            return Task.Factory.StartNew(()=>{});
        }
    }
}