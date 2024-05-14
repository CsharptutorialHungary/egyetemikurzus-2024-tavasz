using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace DDUFSL.XmlManager
{
    internal class XmlManager : IXmlManager
    {
        XmlDocument _document { get; set; }
        public XmlManager() 
        {
            _document = new XmlDocument();
            try
            {
                _document.Load("config.xml");
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                Environment.Exit(0);
                return;
            }
            catch (XmlException e)
            {
                Console.WriteLine(e.Message);
                Environment.Exit(0);
                return;
            }
        }
        public string getDirectoryPath()
        {
            return _document.DocumentElement.SelectSingleNode("Folder").InnerText ?? "";
        }


        public List<string> getFileTypes()
        {
            throw new NotImplementedException();
        }

        public XmlNodeList getFileTypePaths()
        {
            return _document.DocumentElement?.SelectNodes("/FilterConfig/FileTypes/FileType");
        }
    }
}
