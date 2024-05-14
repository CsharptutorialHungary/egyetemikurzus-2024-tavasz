using System.Reflection.Metadata;
using System.Xml;

using DDUFSL.FileFilter;
using DDUFSL.FileOrganizer;
using DDUFSL.XmlManager;

internal class Program
{
    async private static Task Main(string[] args)
    {
        XmlManager xmlManager = new XmlManager();

        string? folderPath = xmlManager.getDirectoryPath();
        if (folderPath == null)
        {
            Console.WriteLine("Nincs workng directory megadva!");
            return;
        }

        FileFilter fileFilter = new FileFilter();
        List<Dictionary<string, string>> filteredFiles = fileFilter.Filter(folderPath);
        if (filteredFiles.Count == 0)
        {
            Console.WriteLine("Nincs a filterek alapján rendezhető file.");
            return;
        }

        FileOrganizer fileOrganizer = new FileOrganizer(folderPath, filteredFiles);
        await fileOrganizer.Organize();
    }
}