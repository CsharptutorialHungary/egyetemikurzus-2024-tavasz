using System.Diagnostics;

using DownloadManager.Application;
using DownloadManager.Infrastructure;

namespace DownloadManager.UserInterface
{
    internal class UI
    {
        private readonly IHost _host;
        private readonly Controller _controller;
        private readonly ICommandLoader _commandLoader;

        public UI(IHost host, Controller controller, ICommandLoader commandLoader)
        {
            _host = host;
            _controller = controller;
            _commandLoader = commandLoader;
        }

        public void Run()
        {
            while (true)
            {
                _host.Write($"{_controller.CurrentMode.ToString()}: ");
                string[] input = _host.ReadLine().Trim().Split(' ');
                var commandToExecute = FindCommandByName(input[0]);
                var args = input.Skip(1).ToArray();
                if (commandToExecute != null)
                {
                    try
                    {
                        commandToExecute.Execute(_host, _controller, _controller.CurrentMode, args);
                    }
                    catch (InvalidOperationException invalidOperationException)
                    {
                        _host.WriteLine(invalidOperationException.Message);
                        _host.WriteLine("A parancsról több információért írd be, hogy 'help [parancs_neve]'");
                        Trace.WriteLine(invalidOperationException, "commandexception");
                    }
                    catch (Exception exception)
                    {
                        _host.WriteLine("Váratlan hiba történt :(");
                        Trace.WriteLine(exception, "otherexception");
                    }
                }
                else
                {
                    _host.WriteLine("Ismeretlen parancs. Segítségért írd be, hogy 'help'!");
                }
            }
        }

        private ICommand? FindCommandByName(string commandName)
        {
            return _commandLoader.Commands
                .FirstOrDefault(command => command.Name.Equals(commandName, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}