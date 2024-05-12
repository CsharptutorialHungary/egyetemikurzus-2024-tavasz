using Filemanager.Infrastructure;
using Filemanager.Model;

namespace Filemanager.Commands{
    internal class SetDirectoryCommand : ICommand
    {
        public string Name => "setdir";

        public void Execute(IHost host, string[] args, Cache cache)
        {
            if(args.Length<2)
            {
                host.WriteLine("Missing one argument: target_dir_path");
            } else {
                string path = args[1];
                if(Directory.Exists(path))
                {
                    cache.Target_dir=path;
                    host.WriteLine("The target directory is: " + path);
                    string file_path = path +"/fm_config.json";
                    if(!File.Exists(file_path))
                    {
                        host.WriteLine("... creating fm_config.json");
                        File.Create(file_path);
                    }
                } else {
                    host.WriteLine("Error: the given directory does not exist");
                }
            }
        }
    }
}