using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using V2JQM3.Infrastructure;

namespace V2JQM3.Application
{
    internal class CmdDownload:IMyCmd
    {
        public string Name => "download";

        public void Execute(IHost host, string[] args)
        {
            host.DownloadRssFile();
        }
    }
}
