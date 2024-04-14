using TruthOrDare.Domain.Entities;
using TruthOrDare.Domain.Enums;

namespace TruthOrDare.Application.Ports
{
    /// <summary>
    /// Incoming port to get and manage cards
    /// </summary>
    public interface ICardPort
    {
        Card GetNextCard();
        Card GetNextCard<T>(GameMode gameMode) where T : Card;
        Card GetNextCard(GameMode gameMode);
        Card GetCardById(int id);
        Card GetRandomCard();
        IEnumerable<Card> GetAllCards();
        void GenerateDefaultCards();
        IEnumerable<Card> GetCardsByGameMode(GameMode gameMode);
    }
}
