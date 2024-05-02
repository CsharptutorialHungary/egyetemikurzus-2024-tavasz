using Filemanager.Infrastructure;

namespace Filemanager.Commands{
    internal class SetDirectoryCommand : ICommand
    {
        public string Name => "setdir";

        public void Execute(IHost host, string[] args)
        {
            if(args.Length<2){
                host.WriteLine("Missing one argument: target_dir_path");
            }
        }
    }
}