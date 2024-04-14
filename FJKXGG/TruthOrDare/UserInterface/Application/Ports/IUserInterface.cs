using TruthOrDare.Domain.Entities;
using TruthOrDare.UserInterface.Entities;

namespace TruthOrDare.UserInterface.Application.Ports
{
    public interface IUserInterface
    {

        string AskOptionSelectionQuestion(string question, IEnumerable<string> options);
        object AskOptionSelectionQuestion(string question, IEnumerable<Option> options);

        GameMode AskGameModeSelectionQuestion(IEnumerable<GameMode> gameModes);
        void DisplayCard(Card card);
        void DisplayMessage(string v);
    }
}