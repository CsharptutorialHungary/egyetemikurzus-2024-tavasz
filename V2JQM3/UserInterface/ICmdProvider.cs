using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using V2JQM3.Infrastructure;

namespace V2JQM3.UserInterface
{
    internal interface ICmdProvider
    {
        IMyCmd[] Commands { get; }
    }
}
