using System.Reflection.Metadata;
using System.Xml;

using DDUFSL.FileFilter;

internal class Program
{
    private static void Main(string[] args)
    {
        XmlDocument document = new XmlDocument();
        try
        {
            document.Load("config.xml");
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine(e.Message);
            return;
        }
        catch (XmlException e)
        {
            Console.WriteLine(e.Message);
            return;
        }

        string? folderPath = document.DocumentElement.SelectSingleNode("Folder").InnerText;
        if (folderPath == null)
        {
            Console.WriteLine("Nincs workng directory megadva!");
            return;
        }

        FileFilter fileFilter = new FileFilter();
        List<string> filteredFiles = fileFilter.Filter(folderPath);
        if (filteredFiles.Count == 0)
        {
            Console.WriteLine("Nincs a filterek alapján rendezhető file.");
            return;
        }


    }
}