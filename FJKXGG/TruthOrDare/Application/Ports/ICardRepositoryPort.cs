using TruthOrDare.Domain.Entities;

// DONE: Use Async Task
// TODO: Separate the Read and Write ports
// TODO: document exceptions

namespace TruthOrDare.Application.Ports;

/// <summary>
/// Outgoing port for card database repository. The database module can implement this.
/// </summary>
internal interface ICardRepositoryPort
{
    /// <summary>
    /// Get all cards from every game mode and card type
    /// </summary>
    /// <returns>List of every available card in every game mode and card type</returns>
    Task<IEnumerable<ICard>> GetAllCardsAsync();

    /// <summary>
    /// Create default cards for the game
    /// </summary>
    /// <exception cref="SafeException">Thrown when TODO: ....</exception>
    Task GenerateDefaultCardsAsync();

    /// <summary>
    /// Get all the cards in a specific card type
    /// </summary>
    /// <typeparam name="T">specifies the required card type</typeparam>
    /// <returns>Returns the list of cards in a card type</returns>
    Task<IEnumerable<T>> GetCardsByTypeAsync<T>() where T : ICard;
}