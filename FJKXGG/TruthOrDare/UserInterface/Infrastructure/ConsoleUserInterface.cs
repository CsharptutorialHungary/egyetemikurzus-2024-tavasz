using TruthOrDare.Domain.Entities;
using TruthOrDare.UserInterface.Application;
using TruthOrDare.UserInterface.Domain;

namespace TruthOrDare.UserInterface.Infrastructure;

// TODO: Violates the Singe Responsibility Principle
internal class ConsoleUserInterface : IUserInterfacePort
{

    public GameMode AskGameModeSelectionQuestion(IEnumerable<GameMode> gameModes)
    {
        IEnumerable<Option> options = gameModes.Select(gameMode => new Option(gameMode.Name, gameMode.Description, gameMode));

        // This might create infinite loop
        return AskOptionSelectionQuestion("Please select game mode!", options) as GameMode ?? AskGameModeSelectionQuestion(gameModes);
    }

    public string AskOptionSelectionQuestion(string question, IEnumerable<string> options)
    {
        IEnumerable<Option> optionsAsStrings = options.Select(option => new Option(option, "", option));

        // This might create infinite loop
        return AskOptionSelectionQuestion(question, optionsAsStrings) as string ?? AskOptionSelectionQuestion(question, options);
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
        if (!int.TryParse(Console.ReadLine(), out int response))
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

    public void DisplayCard(ICard card)
    {
        // TODO: move the style selection to parameter
        string separator;
        if (card is DareCard)
        {
            separator = "++++++++++++++++++++++++++++++DARE++++++++++++++++++++++++++++++++";
        }
        else if (card is TruthCard)
        {
            separator = "----------------------------TRUTH----------------------------------";
        }
        else
        {
            separator = "";
        }

        Console.WriteLine();
        Console.WriteLine(separator);
        Console.WriteLine(card.Text);
        Console.WriteLine(separator);
    }

    public void DisplayMessage(string text)
    {
        Console.WriteLine("Message: " + text);
    }

    public void Exit(int exitCode)
    {
        DisplayMessage("Goode bye.");
        Environment.Exit(exitCode);
    }
}
