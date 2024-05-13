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
                if (cache.Stored_folderdefs.Count != 0)
                {
                    string file_path = cache.Target_dir + "/fm_config.json";
                    cache.Stored_folderdefs = [];
                    try
                    {
                        using (FileStream config_stream = File.Open(file_path, FileMode.Truncate))
                        {
                            Serializer serializer = new();
                            await serializer.SerializeToJson(config_stream, [.. cache.Stored_folderdefs]);
                        }
                        host.WriteLine("Config file cleared");
                    }
                    catch (Exception file_exception)
                    {
                        host.WriteLine("Exception occured while trying to open fm_config.json: " + file_exception.Message);
                    }

                }
                else
                {
                    host.WriteLine("Config file was empty.");
                }
            }

        }
    }
}