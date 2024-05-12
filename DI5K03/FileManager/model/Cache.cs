namespace Filemanager.Model
{
    internal class Cache{
        public string Target_dir {get; set;}

        public FolderDef[] Stored_folderdefs {get; set;}

        public Cache(){
            Target_dir = "";
        }
    }
}