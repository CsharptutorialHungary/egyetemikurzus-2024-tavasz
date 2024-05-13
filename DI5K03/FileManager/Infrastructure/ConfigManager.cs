using Filemanager.Model;

namespace Filemanager.Infrastructure
{
    internal class ConfigManager
    {
        public static async Task WriteCachedFolderDefsIntoConfig(IHost host, Cache cache, string target)
        {
            try
            {
                using (FileStream config_stream = File.Open(target, FileMode.Create))
                {
                    Serializer serializer = new();
                    await serializer.SerializeToJson(config_stream, cache.Stored_folderdefs);
                }
            }
            catch (Exception file_exception)
            {
                host.WriteLine("Exception occured while trying to open fm_config.json: " + file_exception.Message);
            }
        }

        public static async Task LoadFolderDefsFromConfigFile(IHost host, Cache cache, string target)
        {
            try
            {
                using (FileStream config_stream = File.OpenRead(target))
                {
                    Serializer serializer = new();
                    cache.Stored_folderdefs = await serializer.DeserializeFromJson(config_stream);
                }
            }
            catch (Exception file_exception)
            {
                host.WriteLine("Exception occured while trying to open fm_config.json: " + file_exception.Message);
            }
        }


    }
}