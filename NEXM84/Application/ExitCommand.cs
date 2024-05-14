using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using NEXM84.Infrastructure;

namespace NEXM84.Application
{
    internal class ExitCommand : Infrastructure.ICommand
    {
        public string Name => "exit";

        public string Description => "Closes the application";

        public void execute(IUiController iUiController, string[] args)
        {
            iUiController.exit();
        }
    }
}
