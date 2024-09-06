using TruthOrDare.Domain.Entities;

// DONE: Separate the Read and Write ports - not required for small projects
// TODO: document exceptions
// DONE: Delete unused methods

namespace TruthOrDare.Application.Ports;

// DONE: Violates the Interface Segregation Principle
// DONE: Violates the CQRS pattern - not required for such small project
// DONE: Violates YAGNI

/// <summary>
/// Incoming port to get and manage cards. The user interface or the REST API module cloud use this port to get and manipulate Cards.
/// </summary>
public interface ICardPort
{
    /// <summary>
    /// Create default cards for the game
    /// </summary>
    /// <exception cref="SafeException">Thrown when TODO: ....</exception>
    void GenerateDefaultCards();

    /// <summary>
    /// Returns the next card from any game mode or card type based on a magical algorithm
    /// </summary>
    /// <returns>The next card</returns>
    ICard GetNextCard();

    /// <summary>
    /// Returns the best next card in a game mode in a specific type of card based on a magical algorithm
    /// </summary>
    /// <typeparam name="T">Type of returned card. It has to be the child of the Card class</typeparam>
    /// <param name="gameMode"></param>
    /// <returns>Returns the next card in a specific card type and game mode</returns>
    T GetNextCard<T>(GameMode gameMode) where T : ICard;

    /// <summary>
    /// Returns a random card from any game mode and type
    /// </summary>
    /// <returns>Returns a random card from any game mode and type</returns>
    ICard GetRandomCard();

    /// <summary>
    /// Returns a random card from a specific game mode and card type
    /// </summary>
    /// <typeparam name="T">Required type of card. It has to implement ICard</typeparam>
    /// <param name="gameMode">Required game mode</param>
    /// <returns>A random card from the specified game mode and card type</returns>
    T GetRandomCard<T>(GameMode gameMode) where T : ICard;

    /// <summary>
    /// List of all the cards in the database in any type or game mode
    /// </summary>
    /// <returns>Returns the list of all the cards in the database in any type or game mode</returns>
    IEnumerable<ICard> GetAllCards();
}
