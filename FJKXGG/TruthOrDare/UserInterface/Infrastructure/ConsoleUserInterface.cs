using System;
using System.Collections.Generic;

using TruthOrDare.Domain.Entities;
using TruthOrDare.UserInterface.Application.Ports;
using TruthOrDare.UserInterface.Entities;
namespace TruthOrDare.UserInterface.Infrastructure
{
    public class ConsoleUserInterface : IUserInterface
    {

        public GameMode AskGameModeSelectionQuestion(IEnumerable<GameMode> gameModes)
        {
            IEnumerable<Option> options = gameModes.Select(gameMode => new Option
            {
                Name = gameMode.Name,
                Description = gameMode.Description,
                Value = gameMode
            });

            // This might create infinite loop
            return AskOptionSelectionQuestion("Please select game mode!", options) as GameMode ?? AskGameModeSelectionQuestion(gameModes);
        }

        public string AskOptionSelectionQuestion(string question, IEnumerable<string> options)
        {
            IEnumerable<Option> optionsString = options.Select(option => new Option
            {
                Name = option,
                Description = "",
                Value = option
            });

            // This might create infinite loop
            return AskOptionSelectionQuestion(question, optionsString) as string ?? AskOptionSelectionQuestion(question, options);
        }

        public object AskOptionSelectionQuestion(string question, IEnumerable<Option> options)
        {
            Option[] optionsArray = options.ToArray();

            Console.WriteLine("\n" + question);
            foreach (Option option in optionsArray)
            {
                Console.WriteLine($"{Array.IndexOf(optionsArray, option)} - {option.Name}: {option.Description}");
            }

            Console.Write("Enter the number of the option you want to select: ");
            int response;
            if (!int.TryParse(Console.ReadLine(), out response))
            {
                Console.WriteLine("Invalid input, please try again.");
                return AskOptionSelectionQuestion(question, options);
            }

            try
            {
                return optionsArray[response].Value;
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Invalid option, please try again.");
                return AskOptionSelectionQuestion(question, options);
            }
        }

        public void DisplayCard(Card card)
        {
            Console.WriteLine(card.Text);
        }

        public void DisplayMessage(string text)
        {
            Console.WriteLine("Message: " + text);
        }
    }
}
