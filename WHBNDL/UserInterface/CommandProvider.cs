using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WHBNDL.Application;
using WHBNDL.Database;
using WHBNDL.Infrastructure;

namespace WHBNDL.UserInterface
{
    internal class CommandProvider
    {
        public IShellCommand[] Commands
        {
            get;
        }
       public CommandProvider(MemoryDatabase database)
        {
            Commands = new IShellCommand[]
            {
                new ExitCommand(),
                new HelpCommand(),
                new DescriptionCommand(),
                new StartCommand(database),
                new ListCommand(database),
                new BestResultCommand(database)
            };
        }
    }
}
