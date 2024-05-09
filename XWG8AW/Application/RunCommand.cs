using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XWG8AW.Infrastructure;

namespace XWG8AW.Application
{
    internal class RunCommand : IShellCommand
    {
        public string Name => "run";

        public void Execute(IHost host, string[] args)
        {
            host.WriteLine("Adj meg egy parancsot:\t(help)");
        }
    }
}
