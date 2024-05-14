using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NEXM84.UserInterface;

namespace NEXM84.Infrastructure
{
    internal interface IUiController

    {
        string readLine();

        string ReadPassword();

        void writeLine(string message);

        void Write(string message)
            => Console.Write(message);

        static void DrawWaitTag()
            => Console.Write("\n> ");

        static void DrawInputTag()
            => Console.Write("=> ");

        ICommand? findCommandName(string commandName);

        public ICommand[] getCommands();
        void exit();
        FileBrowser GetFileBrowser();
    }
}
