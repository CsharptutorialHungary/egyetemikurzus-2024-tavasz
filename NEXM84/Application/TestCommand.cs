using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using NEXM84.Infrastructure;

namespace NEXM84.Application
{
    internal class TestCommand : Infrastructure.ICommand
    {
        public string Name => "test";

        public string Description => "for testing purposes";

        public void execute(IUiController iUiController, string[] args)
        {
            Console.WriteLine("test command has been found and executed");
        }
    }
}
