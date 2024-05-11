using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BKYZSA.Infrastructure;

namespace BKYZSA.UserInterface
{
    internal class Ui
    {
        private readonly CommandLoader _commandLoader;
        public static bool ModelRunning = false;

        public Ui(CommandLoader commandLoader)
        {
            _commandLoader = commandLoader;
        }

        public void Run()
        {
            Console.WriteLine("Üdvözöllek a ConsoleGPT Playgroundban!\n" +
                "Első lépésként használd a \"help\" parancsot és tájékozódj az elérhető " +
                "parancsokról.\n");
            while (true)
            {
                if(ModelRunning)
                    continue;

                Console.Write("> ");
                string? input = Console.ReadLine();
                string[] splittedInput = input.Split(' ');
                ICommand? command = FindCommand(splittedInput[0]);

                if (command != null)
                {
                    try
                    {
                        if (command.Name.Equals("startinstance", StringComparison.CurrentCultureIgnoreCase))
                            ModelRunning = true;
                        command.Execute(splittedInput);
                    }
                    catch (Exception ex) 
                    {
                        Console.WriteLine("Hiba történt!");
                        Trace.WriteLine(ex, "commandexception");
                    }
                }
                else
                {
                    Console.WriteLine("Nincs ilyen parancs. Használd a \"help\" parancsot a további információkért.\n");
                }

            }
        }

        private ICommand? FindCommand(string v)
        {
            return _commandLoader.Commands.FirstOrDefault(command => command.Name.Equals(v, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
