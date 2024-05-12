using Filemanager.Model;

namespace Filemanager.Infrastructure{
    internal interface ISerializer{
        public void SerializeToJson(Stream target, FolderDef[] folderDefs);

        public Task<FolderDef[]> DeserializeFromJson(Stream from);
    }
}