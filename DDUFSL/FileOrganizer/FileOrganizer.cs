using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DDUFSL.FileOrganizer
{
    internal class FileOrganizer
    {
        string mainDirectory { get; set; }
        List<Dictionary<string, string>> filteredFiles { get; set; }
        public FileOrganizer(string pathDirectory, List<Dictionary<string, string>> filteredFilesList) 
        {
            mainDirectory = pathDirectory;
            filteredFiles = filteredFilesList;
        }
        async public Task Organize()
        {
            List<Task> tasks = new List<Task>();
            foreach (var file in filteredFiles)
            {
                try
                {
                    tasks.Add(MoveFileAsync(file["filePath"], Path.Join(mainDirectory, file["newPath"], Path.GetFileName(file["filePath"]))));
                } catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            await Task.WhenAll(tasks);
            Console.WriteLine("Rendezés sikeres");
            return;


        }

        static Task MoveFileAsync(string sourceFile, string destinationFile)
        {
            return Task.Run(() => {
                Directory.CreateDirectory(Path.GetDirectoryName(destinationFile));
                File.Move(sourceFile, destinationFile);
             });
        }
    }
}
