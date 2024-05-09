using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XWG8AW.Infrastructure;

namespace XWG8AW.UserInterface
{
    internal interface ICommandProvider
    {
        IShellCommand[] Commands { get; }
    }
}
