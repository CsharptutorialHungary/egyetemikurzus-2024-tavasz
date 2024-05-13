using Filemanager.Model;

namespace Filemanager.Infrastructure{
    internal interface ISerializer{
        public Task SerializeToJson(Stream target, List<FolderDef> folderDefs);

        public Task<List<FolderDef>> DeserializeFromJson(Stream from);
    }
}