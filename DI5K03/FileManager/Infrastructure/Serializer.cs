using System.Text.Json;

using Filemanager.Model;

namespace Filemanager.Infrastructure
{
    internal class Serializer : ISerializer
    {
        public static readonly Serializer Instance = new();

        private readonly JsonSerializerOptions _options;

        private Serializer()
        {
            _options = new JsonSerializerOptions { AllowTrailingCommas = true, WriteIndented = true };
        }
        public async Task<List<FolderDef>> DeserializeFromJson(Stream from)
        {
            FolderDef[]? folders = await JsonSerializer.DeserializeAsync<FolderDef[]>(from);
            return folders != null ? new List<FolderDef>(folders) : ([]);


        }

        public async Task SerializeToJson(Stream target, List<FolderDef> folderDefs)
        {
            await JsonSerializer.SerializeAsync<FolderDef[]>(target, [.. folderDefs], _options);
        }
    }
}