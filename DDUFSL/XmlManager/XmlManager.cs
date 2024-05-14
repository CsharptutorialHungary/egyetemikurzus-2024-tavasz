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
    /// <summary>
    /// A 'config.xml' file-t az executable mellé kell rakni
    /// 
    /// Egy példa struktúra:
    /// 
    /// <FilterConfig>
    ///     <Folder>C:\Users\GreG\Downloads\Qpak-server\mods</Folder>   //A mappa útvonala amit rendezni szeretnél
    ///     <FileTypes> //FileTípusok
    ///         <FileType> //Egy FileTípus object
    ///             <Extension>.jar</Extension> //FileTípus kiterjesztése
    ///             <Path>\java\jars</Path> //Megfelelő típusú file-ok mozgatási helye
    ///         </FileType>
    ///         <FileType>
    ///             <Extension>.zip</Extension>
    ///             <Path>\zips</Path>
    ///         </FileType>
    ///         <FileType>
    ///             <Extension>.exe</Extension>
    ///             <Path>\executables</Path>
    ///         </FileType>
    ///     </FileTypes>
    /// </FilterConfig>
    /// 
    /// 
    /// </summary>
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
