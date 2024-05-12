using Filemanager.Infrastructure;
using Filemanager.Model;

namespace Filemanager.Commands
{
    internal class ClearAllCommand : ICommand
    {
        public string Name => "clear-all";

        public async void ExecuteAsync(IHost host, string[] args, Cache cache)
        {
            if (cache.Target_dir.Equals(""))
            {
                host.WriteLine("Can't execute command: no target directory selected yet");
            }
            else
            {
                if (cache.Stored_folderdefs.Length != 0)
                {
                    string file_path = cache.Target_dir + "/fm_config.json";
                    cache.Stored_folderdefs = [];
                    using (FileStream config_stream = File.OpenWrite(file_path))
                    {
                        Serializer serializer = new();
                        await serializer.SerializeToJson(config_stream, cache.Stored_folderdefs);
                    }
                    host.WriteLine("Config file cleared");
                }
                else
                {
                    host.WriteLine("Config file was empty.");
                }
            }

        }
    }
}