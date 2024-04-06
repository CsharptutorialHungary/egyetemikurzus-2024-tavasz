using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WHBNDL.Infrastructure;

namespace WHBNDL.UserInterface
{
    internal class Host : IHost
    {
        public void Exit()
            => Environment.Exit(0);

        public string ReadLine()
            => Console.ReadLine() ?? throw new InvalidOperationException();

        public void WriteLine(string message)
         => Console.WriteLine(message);
    }
}
