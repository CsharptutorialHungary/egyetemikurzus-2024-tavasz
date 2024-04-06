using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHBNDL.Infrastructure
{
    internal interface IHost
    {
        string ReadLine();
        void WriteLine(string message);
        void Exit();
    }
}
