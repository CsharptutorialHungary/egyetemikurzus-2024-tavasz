using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using V2JQM3.Infrastructure;

namespace V2JQM3.Application
{
    internal class CmdLoad:IMyCmd
    {
        public string Name => "load";

        public void Execute(IHost host, string[] args)
        {
            host.LoadSavedRssFiles();
        }
    }
}
