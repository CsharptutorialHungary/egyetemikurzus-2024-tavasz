using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XWG8AW.Application;
using XWG8AW.Infrastructure;

namespace XWG8AW.UserInterface
{
    [Obsolete]
    internal class CommandProvider : ICommandProvider
    {
        public IShellCommand[] Commands
        {
            get;
        }

        public CommandProvider()
        {
            Commands = new IShellCommand[]
                {
                    new ExitCommand(),
                    new RunCommand(),
                    new BestCommand(),
                    new ClearCommand(),
                    new HelpCommand(),
                    new StartCommand(),
                    new ListCommand(),
                };
        }
    }
}
