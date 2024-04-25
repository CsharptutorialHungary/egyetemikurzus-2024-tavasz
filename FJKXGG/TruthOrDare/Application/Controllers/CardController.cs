using TruthOrDare.Application.Ports;
using TruthOrDare.Domain.Entities;
using TruthOrDare.Domain.Exceptions;
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
            _cardDbPort.GenerateDefaultCardsAsync();
            return;
        }

        public IEnumerable<ICard> GetAllCards()
        {
            IEnumerable<ICard> cards = _cardDbPort.GetAllCardsAsync().Result;
            return cards;
        }

        public IEnumerable<ICard> GetCardsByGameMode(GameMode gameMode)
        {
            return _cardDbPort.GetAllCardsAsync().Result.Where(c => c.GameMode == gameMode);
        }

        public ICard GetNextCard()
        {
            return GetRandomCard();
        }

        public ICard GetNextCard(GameMode gameMode)
        {
            return GetRandomCard(gameMode);
        }

        public T GetNextCard<T>(GameMode gameMode) where T : ICard
        {
            return GetRandomCard<T>(gameMode);
        }

        public ICard GetRandomCard() => _cardDbPort.GetAllCardsAsync().Result
                .ElementAtOrDefault(new Random()
                .Next(0, _cardDbPort.GetAllCardsAsync()
                .Result.Count()))
                ?? throw new SafeException("Failed to get random card.");

        public ICard GetRandomCard(GameMode gameMode)
        {
            IEnumerable<ICard> cardsByGameMode =  _cardDbPort.GetAllCardsAsync().Result
            .Where(c => c.GameMode == gameMode);
            return cardsByGameMode
                .ElementAtOrDefault(new Random()
                .Next(0, cardsByGameMode.Count()))
                ?? throw new SafeException("Failed to get random card by game mode.");
        }

        public T GetRandomCard<T>(GameMode gameMode) where T : ICard
        {
            
            var cardsByGameMode = _cardDbPort.GetCardsByTypeAsync<T>().Result
                .Where(c => c.GameMode == gameMode);

            return cardsByGameMode
                .ElementAtOrDefault(new Random()
                .Next(0, cardsByGameMode.Count()))
                ?? throw new SafeException("Failed to get random card by type and game mode.");
        }
    }
}