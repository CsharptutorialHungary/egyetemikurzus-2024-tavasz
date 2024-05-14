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
            XmlNodeList? typeNodes = document.DocumentElement.SelectNodes("/FilterConfig/FileTypes/FileType");
            if (typeNodes == null || typeNodes.Count == 0)
            {
                Console.WriteLine("Nincs tipus szures");
                return;
            } else
            {
                foreach (XmlNode node in typeNodes)
                {
                    fileTypes.Add(node.InnerText);
                }
            }

        }

        public void Filter()
        {
            throw new NotImplementedException();
        }
    }
}
