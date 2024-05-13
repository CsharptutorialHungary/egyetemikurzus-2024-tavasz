using WHBNDL.Infrastructure;

namespace WHBNDL.UserInterface
{
    internal class UI
    {
        private readonly CommandProvider _commandProvider;
        private readonly IHost _host;
        public UI(CommandProvider commandProvider, IHost host)
        {
            _commandProvider = commandProvider;
            _host = host;
        }
        public void Run()
        {
            while (true)
            {
                string input = _host.ReadLine();
                string[] splittedInput = input.Split(' ');
                IShellCommand? commandToExecute = FindCommandName(splittedInput[0]);
                if (commandToExecute != null)
                {
                    commandToExecute.Execute(_host, splittedInput);
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
