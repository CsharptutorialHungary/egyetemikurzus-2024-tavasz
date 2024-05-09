using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XWG8AW.Infrastructure;

namespace XWG8AW.Application
{
    internal class ClearCommand : IShellCommand
    {
        public string Name => "clear";

        public void Execute(IHost host, string[] args)
        {
            Console.Clear();
            host.WriteLine("Adj meg egy parancsot:\t(help)");
        }
    }
}
