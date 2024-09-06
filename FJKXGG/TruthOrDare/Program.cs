using TruthOrDare.Application.Controllers;
using TruthOrDare.Infrastructure;
using TruthOrDare.Infrastructure.Repositories;
using TruthOrDare.UserInterface.Application;
using TruthOrDare.UserInterface.Infrastructure;

// DONE: Exception handling: https://www.reddit.com/r/csharp/comments/ns7m5g/java_exceptions_vs_c_ones_how_do_i_know_ive/
// DONE: use null checks where required
// DONE: Check Messages and Warnings
// DONE: Check design principles
// TODO: Fix design principles
// TODO: Create unit tests

internal class Program
{
    private static void Main(string[] args)
    {
        try
        {
            // TODO: Refactor it to use auto dependency injection pattern: https://www.youtube.com/watch?v=M1jxLQu40qo
            var userInterfaceController = new UserInterfaceController(
                new ConsoleUserInterface(),
                new CardController(new CardRepository(new JsonCardLoader(), new JsonCardWriter(new GameModeRepository()))),
                new GameModeController(new GameModeRepository())
                );

            userInterfaceController.Run();
            Environment.Exit(0);
        }
        catch (Exception)
        {
            // In this layer there is no logging or Console interface, so I do not know how to handle an unexpected exception.
            Environment.Exit(1);
        }

    }
}