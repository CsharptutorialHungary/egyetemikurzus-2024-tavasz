using TruthOrDare.Domain.Entities;
using TruthOrDare.UserInterface.Domain;

namespace TruthOrDare.UserInterface.Application
{
    // TODO: Violates the Interface Segregation Principle

    /// <summary>
    /// Outgoing port to connect the exact user interface implementation to the user interface controller
    /// </summary>
    internal interface IUserInterfacePort
    {
        /// <summary>
        /// Displays a question and a list of options to the user and returns the selected option
        /// </summary>
        /// <param name="question">The question you want to ask for the user</param>
        /// <param name="options">The available options name the user cloud choose</param>
        /// <returns>The selected option's name as a string</returns>
        string AskOptionSelectionQuestion(string question, IEnumerable<string> options);

        /// <summary>
        /// Displays a question and a list of options to the user and returns the selected option
        /// </summary>
        /// <param name="question">The question you want to ask for the user</param>
        /// <param name="options">The available options as Option objects the user cloud choose</param>
        /// <returns>The selected option's Value object</returns>
        object AskOptionSelectionQuestion(string question, IEnumerable<Option> options);

        /// <summary>
        /// Asks the user to select a game mode from a list of available game modes
        /// </summary>
        /// <param name="gameModes">List of game modes to show the user</param>
        /// <returns>The selected game mode</returns>
        GameMode AskGameModeSelectionQuestion(IEnumerable<GameMode> gameModes);

        /// <summary>
        /// Displays a card to the user
        /// </summary>
        /// <param name="card">Card to display</param>
        void DisplayCard(ICard card);

        /// <summary>
        /// Displays a message to the user
        /// </summary>
        /// <param name="message"></param>
        void DisplayMessage(string message);

        /// <summary>
        /// tTerminates the application and returns the exit code
        /// </summary>
        /// <param name="exitCode">exit code to return to the operation system</param>
        void Exit(int exitCode);
    }
}