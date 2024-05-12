using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XWG8AW.Infrastructure
{
    internal interface IShellCommand
    {
        string Name { get; }
        void Execute(IHost host, string[] args);
    }
}
