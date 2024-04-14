using TruthOrDare.Application.Ports;
using TruthOrDare.Application.Controllers;
using TruthOrDare.Infrastructure;
using TruthOrDare.UserInterface.Application;
using TruthOrDare.UserInterface.Domain;
using TruthOrDare.UserInterface.Infrastructure;

internal class Program
{
    private static void Main(string[] args)
    {
        // TODO: Expetion handling: https://www.reddit.com/r/csharp/comments/ns7m5g/java_exceptions_vs_c_ones_how_do_i_know_ive/
        var userInterfaceController = new UserInterfaceController(
            new UserInterfaceService(),
            new ConsoleUserInterface(),
            new CardController(new CardRepository(new JsonCardLoader(), new JsonCardWriter(new GameModeRepository()))),
            new GameModeController(new GameModeRepository())
        );
        userInterfaceController.Run();
        Environment.Exit(0);
    }
}