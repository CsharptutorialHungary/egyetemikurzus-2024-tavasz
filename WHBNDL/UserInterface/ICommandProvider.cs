using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WHBNDL.Infrastructure;

namespace WHBNDL.UserInterface
{
    internal interface ICommandProvider
    {
        IShellCommand[] Commands { get; }
    }
}
