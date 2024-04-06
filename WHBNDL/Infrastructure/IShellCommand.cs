using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WHBNDL.Infrastructure
{
    internal interface IShellCommand
    {
        string Name { get; }
        void Execute(IHost host, string[] args);
    }
}
