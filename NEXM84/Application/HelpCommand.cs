using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NEXM84.Infrastructure;
using NEXM84.UserInterface;

namespace NEXM84.Application
{
    internal class HelpCommand : ICommand
    {
        public string Name => "help";

        public string Description => "Get help about the available commands.";


        public void execute(IUiController iUiController, string[] args)
        {
            ICommand[] commands = iUiController.getCommands();
            iUiController.Write(helpBuilder(commands));
        }

        private string helpBuilder(ICommand[] commands)
        {
            StringBuilder helpMessageBuilder = new StringBuilder("\nCommands and Descriptions\n" +
                                                                 "------------------------------------------------------\n");
            int maxCommandNameLength = commands.Max(c => c.Name.Length);
            foreach (var command in commands)
            {
                string paddedCommandName = command.Name.PadRight(maxCommandNameLength);
                helpMessageBuilder.AppendLine($"| [{paddedCommandName}] " + $"{command.Description}");
            }
            helpMessageBuilder.AppendLine("------------------------------------------------------\n");
            return helpMessageBuilder.ToString();
        }


    }
}
