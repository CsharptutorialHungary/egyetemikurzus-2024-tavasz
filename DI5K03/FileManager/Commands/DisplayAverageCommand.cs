using Filemanager.Infrastructure;
using Filemanager.Model;

namespace Filemanager.Commands
{
    internal class DisplayAverageCommand : AbstractSynchronousCommand
    {
        public override string Name => "avg-extensions";

        public override void Execute(IHost host, string[] args, Cache cache)
        {
            if (cache.Stored_folderdefs.Count == 0)
            {
                host.WriteLine("There are no Folder definitions for this directory");
            }
            double average = cache.Stored_folderdefs.Average(folderdef => folderdef.Types.Length);
            host.WriteLine("The average number of extensions in a folder definition is " + average);
        }
    }
}