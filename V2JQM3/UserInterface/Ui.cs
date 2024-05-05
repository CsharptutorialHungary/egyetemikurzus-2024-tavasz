using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using V2JQM3.Infrastructure;

namespace V2JQM3.UserInterface
{
    internal class Ui
    {
        private readonly ICmdProvider _commandProvider;
        private readonly IHost _host;

        public Ui(ICmdProvider commandProvider, IHost host)
        {
            _commandProvider = commandProvider;
            _host = host;
        }

        public void Run()
        {
            _host.Help();
            while (true)
            {
                _host.Write("main> ");
                string input = _host.ReadLine();
                string[] splittedInput = input.Split(' ');
                IMyCmd? commandToExecute = FindCommandName(splittedInput[0]);
                if (commandToExecute != null)
                {
                    try
                    {
                        commandToExecute.Execute(_host, splittedInput);
                    }
                    catch (Exception ex)
                    {
                        _host.WriteLine("Error while " + commandToExecute.Name + " was running.");
                        Trace.WriteLine(ex, "commandexception");
                    }
                }
            }
        }

        private IMyCmd? FindCommandName(string commandName)
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
