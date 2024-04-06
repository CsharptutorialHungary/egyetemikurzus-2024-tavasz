using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WHBNDL.Infrastructure;

namespace WHBNDL.Application
{
    internal class WelcomeCommand : IShellCommand
    {
        public string Name => "welcome";

        public void Execute(IHost host, string[] args)
        {
            host.WriteLine("üdvözlet");
        }
    }
}
