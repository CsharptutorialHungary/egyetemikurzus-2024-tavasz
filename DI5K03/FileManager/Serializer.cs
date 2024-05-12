using System.Text.Json;

using Filemanager.Infrastructure;
using Filemanager.Model;

namespace Filemanager{
    internal class Serializer : ISerializer
    {
        public async Task<FolderDef[]> DeserializeFromJson(Stream from)
        {
            FolderDef[]? folders = await JsonSerializer.DeserializeAsync<FolderDef[]>(from,new JsonSerializerOptions{AllowTrailingCommas=true});
            return folders == null ? ([]) : folders;
        }

        public void SerializeToJson(Stream target, FolderDef[] folderDefs)
        {
            throw new NotImplementedException();
        }
    }
}