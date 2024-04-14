using TruthOrDare.Application.Ports;
using TruthOrDare.Domain.Entities;
using TruthOrDare.Domain.Enums;

namespace TruthOrDare.Application.Controllers
{
    internal class CardController : ICardPort
    {
        private readonly ICardRepositoryPort _cardDbPort;
        public CardController(ICardRepositoryPort cardDbPort)
        {
            _cardDbPort = cardDbPort;
        }

        public void GenerateDefaultCards()
        {
            _cardDbPort.GenerateDefaultCards();
            return;
        }

        public IEnumerable<Card> GetAllCards()
        {
            var cards = _cardDbPort.GetAllCards();
            return cards;
        }

        public Card GetCardById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Card> GetCardsByGameMode(GameMode gameMode)
        {
            return _cardDbPort.GetAllCards().Where(c => c.GameMode == gameMode);
        }

        public Card GetNextCard()
        {
            return GetRandomCard();
        }

        public Card GetNextCard(GameMode gameMode)
        {
            return GetRandomCard(gameMode);
        }

        public Card GetNextCard<T>(GameMode gameMode) where T : Card
        {
            return GetRandomCard<T>(gameMode);
        }

        public Card GetRandomCard() => _cardDbPort.GetAllCards()
                .ElementAtOrDefault(new Random()
                .Next(0, _cardDbPort.GetAllCards()
                .Count()));

        public Card GetRandomCard(GameMode gameMode)
        {
            var cardsByGameMode = _cardDbPort.GetAllCards()
            .Where(c => c.GameMode == gameMode);
            return cardsByGameMode
                .ElementAtOrDefault(new Random()
                .Next(0, cardsByGameMode.Count()));
        }

        public Card GetRandomCard<T>(GameMode gameMode) where T : Card
        {
            
            var cardsByGameMode = _cardDbPort.GetCardsByType<T>()
                .Where(c => c.GameMode == gameMode);

            return cardsByGameMode
                .ElementAtOrDefault(new Random()
                .Next(0, cardsByGameMode.Count()));
        }
    }
}