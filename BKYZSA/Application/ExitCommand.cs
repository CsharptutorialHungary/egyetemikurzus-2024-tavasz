using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BKYZSA.Infrastructure;

namespace BKYZSA.Commands
{
    internal class ExitCommand : ICommand
    {
        public string Name => "exit";

        public string Description => "Kilépés a programból.";

        public void Execute(string[] args)
        {
            Environment.Exit(0);
        }
    }
}
