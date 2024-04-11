using Filemanager.Model;

namespace Filemanager.Infrastructure{
    internal interface Serializer{
        public void SerializeToJson(Stream target, FolderDef[] folderDefs);

        public FolderDef[] DeserializeFromJson(Stream from);
    }
}