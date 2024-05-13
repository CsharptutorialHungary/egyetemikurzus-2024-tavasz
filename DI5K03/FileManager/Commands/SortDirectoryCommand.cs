

using Filemanager.Infrastructure;
using Filemanager.Model;

namespace Filemanager.Commands{
    internal class SortDirectoryCommand : ICommand
    {
        public string Name => "sortdir";

        public void ExecuteAsync(IHost host, string[] args, Cache cache)
        {
            if(cache.Target_dir.Equals("")){
                host.WriteLine("Can't execute command: no target directory selected yet");
            } else {
                foreach(FolderDef folderDef in cache.Stored_folderdefs){
                    foreach(string file_path in Directory.GetFiles(cache.Target_dir)){

                        string filename = file_path.Split("\\")[^1];
                        string extension = filename.Split(".")[^1];
                        string destination_folder = cache.Target_dir+"\\"+folderDef.Name;
                        if(filename.Equals("fm_config.json"))
                            {
                                continue;
                            }
                        if(!Directory.Exists(destination_folder)){
                            Directory.CreateDirectory(destination_folder);
                        }
                        if(folderDef.Types.Contains(extension)){
                            File.Move(file_path,destination_folder+"\\"+filename);
                            host.WriteLine("-    Moved "+filename+" to "+folderDef.Name);
                        }
                    }
                }
            }
            host.WriteLine("The target directory is sorted.");
        }
    }
}