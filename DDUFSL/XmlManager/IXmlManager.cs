using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DDUFSL.XmlManager
{
    internal interface IXmlManager
    {
        public List<string> getFileTypes();
        public string getDirectoryPath();
        public XmlNodeList getFileTypePaths();
    }
}
