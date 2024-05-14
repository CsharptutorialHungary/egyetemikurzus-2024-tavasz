using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DDUFSL.FileFilter
{
    internal class FileFilter : IFileFilter
    {
        public List<string> fileTypes { get; set; }
        public long maxFileSize { get; set; }

        public FileFilter()
        {
            XmlDocument document = new XmlDocument();

            try
            {
                document.Load("config.xml");
            } catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                return;
            } catch (XmlException e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            fileTypes = new List<string>();
            XmlNodeList? typeNodes = document.DocumentElement?.SelectNodes("/FilterConfig/FileTypes/FileType");
            if (typeNodes == null || typeNodes.Count == 0)
            {
                Console.WriteLine("Nincs tipus szures");
                return;
            } else
            {
                Console.WriteLine("Talált filterek:");
                foreach (XmlNode node in typeNodes)
                {
                    fileTypes.Add(node.InnerText);
                    Console.WriteLine($"-{node.ChildNodes.Item(0).InnerText}");
                }
            }

        }

        public List<string> Filter(string directoryPath)
        {
            List<string> filteredFiles = new List<string>();
            try
            {
                string[] files = Directory.GetFiles(directoryPath);

                foreach (string file in files)
                {
                    string extension = Path.GetExtension(file);
                    if (fileTypes.Contains(extension))
                    {
                        filteredFiles.Add(file);

                    }
                }
                    return filteredFiles;
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<string>();
            }
        }
    }
}
