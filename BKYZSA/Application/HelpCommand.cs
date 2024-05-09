using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BKYZSA.UserInterface;

namespace BKYZSA.Commands
{
    internal class HelpCommand : ICommand
    {
        public string Name => "help";

        public string Description => "Elérhető parancsok kilistázása.";

        public void Execute(string[] args)
        {
            Console.WriteLine("Elérhető parancsok:\n");

            var commands = new CommandLoader();
            foreach(var command in commands.Commands)
            {
                Console.WriteLine($">>> [{command.Name}] - {command.Description}");
            }

        }
    }
}
