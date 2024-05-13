using System.Text.Json;

using Filemanager.Infrastructure;
using Filemanager.Model;

namespace Filemanager{
    internal class Serializer : ISerializer
    {
        public async Task<List<FolderDef>> DeserializeFromJson(Stream from)
        {
            FolderDef[]? folders = await JsonSerializer.DeserializeAsync<FolderDef[]>(from);
            return folders == null ? new List<FolderDef>(folders) : ([]);


        }

        public async Task SerializeToJson(Stream target, List<FolderDef> folderDefs)
        {
            await JsonSerializer.SerializeAsync<FolderDef[]>(target,[..folderDefs], new JsonSerializerOptions { AllowTrailingCommas = true, WriteIndented = true});
        }
    }
}