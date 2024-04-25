using TruthOrDare.Application.Ports;
using TruthOrDare.Domain.Entities;
using TruthOrDare.Domain.Exceptions;

namespace TruthOrDare.UserInterface.Application
{
    internal class UserInterfaceController(IUserInterfacePort userInterface, ICardPort cardPort, IGameModePort gameModePort)
    {
        private readonly IUserInterfacePort _ui = userInterface;
        private readonly ICardPort _cardPort = cardPort;
        private readonly IGameModePort _gameModePort = gameModePort;

        public int NamedInt { get; set; }

        public void Run()
        {
            // TODO: Use result Pattern instead of Exceptions for flow control: https://youtu.be/2hiKyFi1mgI?si=YXRBkNMDpdAozoh9
            try
            {
                // Welcome message
                _ui.DisplayMessage("Welcome to Truth or Dare");

                // Ensure cards are available
                try
                {
                    _cardPort.GetAllCards();
                }
                catch (SafeException ex)
                {
                    _ui.DisplayMessage(ex.Message);
                    DisplayGenerateCardsQuestion();
                }

                do
                {
                    // Select game mode
                    GameMode? gameMode = SelectGameModeQuestion();

                    PlayGame(gameMode);
                } while (true);
            }
            catch (SafeException ex)
            {
                _ui.DisplayMessage(ex.Message);
            }
        }
        private GameMode? SelectGameModeQuestion()
        {
            IEnumerable<GameMode> gameModes = _gameModePort.GetAllGameModes();
            return gameModes.Any() ? _ui.AskGameModeSelectionQuestion(gameModes) : null;
        }

        private void PlayGame(GameMode? gameMode)
        {
            _ui.DisplayMessage("Press 'T' to get a truth card, 'D' to dare, M to change mode, 'Q' to quit or anything else to get a random card.");
            ConsoleKey key;
            do
            {
                ICard nextCard;
                key = Console.ReadKey().Key;
                switch (key)
                {
                    case ConsoleKey.T:
                        nextCard = gameMode == null ? _cardPort.GetNextCard() : _cardPort.GetNextCard<TruthCard>(gameMode);
                        break;
                    case ConsoleKey.D:
                        nextCard = gameMode == null ? _cardPort.GetNextCard() : _cardPort.GetNextCard<DareCard>(gameMode);
                        break;
                    case ConsoleKey.M:
                        return;
                    case ConsoleKey.Q:
                        _ui.Exit(0);
                        return;
                    default:
                        nextCard = _cardPort.GetRandomCard();
                        break;
                }
                _ui.DisplayCard(nextCard);
                
            } while (true);
        }

        private void DisplayGenerateCardsQuestion()
        {
            var selectedOption = _ui.AskOptionSelectionQuestion("Would you like to generate default card structure?", [
                    "Yes",
                    "No"
                        ]);
            switch (selectedOption)
            {
                case "Yes":
                    _cardPort.GenerateDefaultCards();
                    _ui.DisplayMessage("Default cards generated. Please replace them with your own cards!");
                    return;
                case "No":
                    _ui.Exit(0);
                    return;
            }
        }
    }
}
