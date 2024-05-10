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
            host.WriteLine( "help\t-\tKiírja az összes parancsot.\n" +
                            "list\t-\tListázza az eddig elmentet játékokat (Játékos/Eredmény)\n" +
                            "start\t-\tÚj játék indítása\n" +
                            "exit\t-\tKilépés a programból\n" +
                            "best\t-\tKiírja a megadott játékos legjobb eredményét\n" +
                            "clear\t-\tKiüríti a konzolt\n");
        }
    }
}
