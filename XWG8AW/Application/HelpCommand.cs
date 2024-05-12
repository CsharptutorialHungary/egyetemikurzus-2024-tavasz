using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XWG8AW.Infrastructure;

namespace XWG8AW.Application
{
    internal class HelpCommand : IShellCommand
    {
        public string Name => "help";

        public void Execute(IHost host, string[] args)
        {
            host.WriteLine( "help\t-\tKiirja az osszes parancsot.\n" +
                            "list\t-\tListazza az eddig elmentet jatekokat csokkeno sorrendben (Jatekos/Eredmeny)\n" +
                            "start\t-\tUj jatek inditasa\n" +
                            "exit\t-\tKilepes a programbol\n" +
                            "best\t-\tKiirja a megadott jatekos legjobb eredmenyet\n" +
                            "clear\t-\tKiuriti a konzolt\n");
        }
    }
}
