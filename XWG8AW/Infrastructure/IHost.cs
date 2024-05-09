using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XWG8AW.Infrastructure
{
    internal interface IHost
    {
        string ReadLine();
        void WriteLine(string message);
        void Write(string message)
            => Console.Write(message);
        void Exit();
    }
}
