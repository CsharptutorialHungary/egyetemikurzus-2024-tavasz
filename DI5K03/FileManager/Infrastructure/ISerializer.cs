using Filemanager.Model;

namespace Filemanager.Infrastructure{
    internal interface ISerializer{
        public Task SerializeToJson(Stream target, FolderDef[] folderDefs);

        public Task<FolderDef[]> DeserializeFromJson(Stream from);
    }
}