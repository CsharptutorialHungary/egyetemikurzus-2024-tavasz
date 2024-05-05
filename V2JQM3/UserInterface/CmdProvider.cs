using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using V2JQM3.Application;
using V2JQM3.Infrastructure;

namespace V2JQM3.UserInterface
{
    internal class CmdProvider:ICmdProvider
    {
        public IMyCmd[] Commands
        {
            get;
        }

        public CmdProvider()
        {
            Commands = new IMyCmd[]
                    {
                    new CmdExit(),
                    new CmdDisplay(),
                    new CmdDownload(),
                    new CmdLoad(),
                    new CmdHelp()
                    };
        }
    }
}
