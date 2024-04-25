﻿using TruthOrDare.Application.Ports;
using TruthOrDare.Application.Controllers;
using TruthOrDare.Infrastructure;
using TruthOrDare.UserInterface.Application;
using TruthOrDare.UserInterface.Domain;
using TruthOrDare.UserInterface.Infrastructure;

// DONE: Exception handling: https://www.reddit.com/r/csharp/comments/ns7m5g/java_exceptions_vs_c_ones_how_do_i_know_ive/
// DONE: use null checks where required
// TODO: Check design principles

internal class Program
{
    private static void Main(string[] args)
    {
        try
        {
            // TODO: Refactor it to auto dependency injection pattern: https://www.youtube.com/watch?v=M1jxLQu40qo
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