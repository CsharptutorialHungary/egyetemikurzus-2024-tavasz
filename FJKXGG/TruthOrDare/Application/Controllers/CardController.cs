using TruthOrDare.Application.Ports;
using TruthOrDare.Domain.Entities;
using TruthOrDare.Domain.Exceptions;

namespace TruthOrDare.Application.Controllers;

// TODO: Violates Single Responsibility Principle
// DONE: Violates YAGNI
internal class CardController(ICardRepositoryPort cardDbPort) : ICardPort
{
    private readonly ICardRepositoryPort _cardDbPort = cardDbPort;

    public void GenerateDefaultCards() => _cardDbPort.GenerateDefaultCardsAsync();

    public IEnumerable<ICard> GetAllCards() => _cardDbPort.GetAllCardsAsync().Result;

    public ICard GetNextCard() => GetRandomCard();

    public T GetNextCard<T>(GameMode gameMode) where T : ICard => GetRandomCard<T>(gameMode);

    public ICard GetRandomCard() => _cardDbPort.GetAllCardsAsync().Result
            .ElementAtOrDefault(new Random()
            .Next(0, _cardDbPort.GetAllCardsAsync()
            .Result.Count()))
            ?? throw new SafeException("Failed to get random card.");

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