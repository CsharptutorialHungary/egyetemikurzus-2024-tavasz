using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V2JQM3.Infrastructure
{
    internal interface IMyCmd
    {
        string Name { get; }
        void Execute(IHost host, string[] args);
    }

}
