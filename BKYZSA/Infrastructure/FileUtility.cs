using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKYZSA.Infrastructure
{
    internal class FileUtility
    {
        public static FileInfo[]? GetFilesByPattern(string pattern)
        {
            DirectoryInfo directoryInfo = new(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));

            FileInfo[] files = directoryInfo.GetFiles(pattern);

            return files.Length == 0 ? null : files;
        }

        public static void ListFiles(FileInfo[] files)
        {
            Console.WriteLine("A visszanézhető beszélgetéseid:");

            // TODO: listázza a beszélgetés jsonokat + vmi id
            for (int i = 0; i < files.Length; i++)
            {
                Console.WriteLine($"[{i}] - {files[i].Name}");
            }
        }
    }
}
