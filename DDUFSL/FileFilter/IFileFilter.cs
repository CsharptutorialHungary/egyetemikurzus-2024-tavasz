using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDUFSL.FileFilter
{
    internal interface IFileFilter
    {
        public List<Dictionary<string, string>> Filter(string directoryFilter);
    }
}
