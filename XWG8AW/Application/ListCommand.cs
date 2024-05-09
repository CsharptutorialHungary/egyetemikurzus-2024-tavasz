using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XWG8AW.Infrastructure;

namespace XWG8AW.Application
{
    internal class ListCommand : IShellCommand
    {
        public string Name => "list";

        public void Execute(IHost host, string[] args)
        {
            throw new NotImplementedException();
        }
    }
}
