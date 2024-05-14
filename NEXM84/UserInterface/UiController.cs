using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NEXM84.Infrastructure;
using NEXM84.Utility;

namespace NEXM84.UserInterface
{
    internal class UiController : IUiController
    {
        private CommandLoader commandLoader;
        private readonly FileBrowser fileBrowser;

        public UiController ()
        {
            commandLoader = new CommandLoader ();
            fileBrowser = new FileBrowser();
        }
        
        public string readLine()
            => Console.ReadLine() ?? throw new InvalidOperationException();

        public void writeLine ( string message )
            => Console.WriteLine(message);

        public ICommand? findCommandName(string commandName)
        {
            foreach (var command in commandLoader.commands)
            {
                if (command.Name.Equals(commandName, StringComparison.CurrentCultureIgnoreCase))
                {
                    return command;
                }
            }
            return null;
        }

        public void exit()
            => Environment.Exit(0);

        public ICommand[] getCommands()
        {
            return commandLoader.commands;
        }

        public FileBrowser GetFileBrowser()
        {
            return this.fileBrowser;
        }

        string IUiController.ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);
                if (!char.IsControl(key.KeyChar))
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }

                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Remove(password.Length - 1);
                    Console.Write("\b \b");
                }
            }
            while (key.Key != ConsoleKey.Enter);

            Console.WriteLine();
            return password;
        }
    }
}
