using System.Diagnostics;

using TruthOrDare.Application.Ports;
using TruthOrDare.Domain.Entities;
using TruthOrDare.Domain.Enums;
using TruthOrDare.UserInterface.Application.Ports;
using TruthOrDare.UserInterface.Domain;

namespace TruthOrDare.UserInterface.Application
{
    internal class UserInterfaceController
    {
        private readonly UserInterfaceService _userInterfaceService;
        private readonly IUserInterface _ui;
        private readonly ICardPort _cardPort;
        private readonly IGameModePort _gameModePort;

        public UserInterfaceController(UserInterfaceService userInterfaceService, IUserInterface userInterface, ICardPort cardPort, IGameModePort gameModePort)
        {
            _userInterfaceService = userInterfaceService;
            _ui = userInterface;
            _cardPort = cardPort;
            _gameModePort = gameModePort;
        }

        public int NamedInt { get; set; }

        public void Run()
        {
            // Welcome message
            _ui.DisplayMessage("Welcome to Truth or Dare");

            // Check if cards are not available
            if (!_cardPort.GetAllCards().Any())
            {
                _ui.DisplayMessage("No cards found.");
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
                        return;
                }
            }

            GameMode gameMode = null;
            IEnumerable<GameMode> gameModes = _gameModePort.GetAllGameModes();
            if (gameModes.Any())
                gameMode = _ui.AskGameModeSelectionQuestion(gameModes);
            
            _ui.DisplayMessage("Press any key to get another card or 'q' to quit");
            ConsoleKey key;
            do
            {
                Card nextCard;
                key = Console.ReadKey().Key;
                switch (key)
                {
                    case ConsoleKey.T:
                        nextCard = gameMode == null ? _cardPort.GetNextCard() : _cardPort.GetNextCard<TruthCard>(gameMode);
                        break;
                    case ConsoleKey.D:
                        nextCard = gameMode == null ? _cardPort.GetNextCard() : _cardPort.GetNextCard<DareCard>( gameMode);
                        break;
                    default:
                        nextCard = _cardPort.GetRandomCard();
                        break;
                }
                _ui.DisplayCard(nextCard);
            } while (key == ConsoleKey.Q);
        }
    }
}
