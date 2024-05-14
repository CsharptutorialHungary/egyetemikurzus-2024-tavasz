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
        public List<Dictionary<string, string>> fileTypes { get; set; }
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

            fileTypes = new List<Dictionary<string, string>>();
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
                    fileTypes.Add(new Dictionary<string, string>
                    {
                        {"Extension", node.SelectSingleNode("Extension").InnerText },
                        {"Path", node.SelectSingleNode("Path").InnerText }
                    });
                    Console.WriteLine($"-{node.ChildNodes.Item(0).InnerText}");
                }
            }

        }

        public List<Dictionary<string, string>> Filter(string directoryPath)
        {
            List<Dictionary<string, string>> filteredFiles = new List<Dictionary<string, string>>();
            try
            {
                string[] files = Directory.GetFiles(directoryPath);

                foreach (string file in files)
                {
                    string extension = Path.GetExtension(file);
                    foreach (var type in fileTypes)
                    {
                        if (type["Extension"].Equals(extension))
                        {
                            filteredFiles.Add(new Dictionary<string, string>
                        {
                            { "filePath", file },
                            { "newPath",  type["Path"]}
                        });
                        }
                    }
                }
                    
                    return filteredFiles;
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<Dictionary<string, string>>();
            }
        }
    }
}
