using Filemanager.Model;
using Filemanager.Infrastructure;

namespace Filemanager.Infrastructure
{
    internal class ConfigManager
    {
        private static readonly Serializer MySerializer = Serializer.Instance;

        public static async Task WriteCachedFolderDefsIntoConfig(IHost host, Cache cache, string target)
        {
            try
            {
                using (FileStream config_stream = File.Open(target, FileMode.Create))
                {
                    await MySerializer.SerializeToJson(config_stream, cache.Stored_folderdefs);
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
                    cache.Stored_folderdefs = await MySerializer.DeserializeFromJson(config_stream);
                }
            }
            catch (Exception file_exception)
            {
                host.WriteLine("Exception occured while trying to open fm_config.json: " + file_exception.Message);
            }
        }


    }
}