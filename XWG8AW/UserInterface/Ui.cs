using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XWG8AW.Infrastructure;

namespace XWG8AW.UserInterface
{
    internal class Ui
    {
        private readonly ICommandProvider _commandProvider;
        private readonly IHost _host;

        public Ui(ICommandProvider commandProvider, IHost host)
        {
            _commandProvider = commandProvider;
            _host = host;
        }

        public void Run()
        {
            int firstStart = 0;

            while (true)
            {

                _host.WriteLine("Adj meg egy parancsot:\t(help)");

                string input = _host.ReadLine();


                string[] splittedInput = input.Split(' ');
                IShellCommand? commandToExecute = FindCommandName(splittedInput[0]);

                if (commandToExecute != null)
                {
                    try
                    {
                        commandToExecute.Execute(_host, splittedInput);
                    }
                    catch (Exception ex)
                    {
                        _host.WriteLine("Hiba tortent");
                        Trace.WriteLine(ex, "commandexception");
                    }
                }
            }
        }

        private IShellCommand? FindCommandName(string commandName)
        {
            foreach (var command in _commandProvider.Commands)
            {
                if (command.Name.Equals(commandName, StringComparison.CurrentCultureIgnoreCase))
                {
                    return command;
                }
            }

            return null;
        }
    }
}
