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
        private Mode CurrentMode { get; set; }

        public UI(IHost host, Controller controller, ICommandLoader commandLoader)
        {
            _host = host;
            _controller = controller;
            _commandLoader = commandLoader;
            CurrentMode = Mode.Home;
        }

        public void Run()
        {
            while (true)
            {
                _host.Write($"{CurrentMode.ToString()}: ");
                string[] input = _host.ReadLine().Split(' ');
                var commandToExecute = FindCommandByName(input[0]);
                if (commandToExecute != null)
                {
                    try
                    {
                        commandToExecute.Execute(_host, _controller, CurrentMode, input);
                    }
                    catch (InvalidOperationException invalidOperationException)
                    {
                        _host.WriteLine(invalidOperationException.Message);
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