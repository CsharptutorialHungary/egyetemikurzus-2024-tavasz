using Filemanager.Infrastructure;
using Filemanager.Model;

namespace Filemanager.Commands
{
    internal class ClearAllCommand : ICommand
    {
        public string Name => "clear-all";

        public async Task ExecuteAsync(IHost host, string[] args, Cache cache)
        {
            if (cache.Target_dir.Equals(""))
            {
                host.WriteLine("Can't execute command: no target directory selected yet");
            }
            else
            {
                if (cache.Stored_folderdefs.Count != 0)
                {
                    string file_path = cache.Target_dir + "/fm_config.json";
                    cache.Stored_folderdefs = [];
                    await ConfigManager.WriteCachedFolderDefsIntoConfig(host, cache, file_path);

                }
                else
                {
                    host.WriteLine("Config file was empty.");
                }
            }

        }
    }
}