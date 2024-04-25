using TruthOrDare.Domain.Entities;

// TODO: Separate the Read and Write ports
// TODO: document exceptions
// TODO: Delete unused methods

namespace TruthOrDare.Application.Ports;

/// <summary>
/// Incoming port to get and manage cards. The user interface or the REST API module cloud use this port to get and menipulate Cards.
/// </summary>
public interface ICardPort
{
    ICard GetNextCard();

    /// <summary>
    /// Returns the best next card in a game mode in a specific type of card based on a magical algorithm
    /// </summary>
    /// <typeparam name="T">Type of returned card. It has to be the child of the Card class</typeparam>
    /// <param name="gameMode"></param>
    /// <returns>Returns the next card in a specific card type and game mode</returns>
    T GetNextCard<T>(GameMode gameMode) where T : ICard;

    /// <summary>
    /// returns the best next card in a specific game mode based on a magical algorithm
    /// </summary>
    /// <param name="gameMode">specify the required game mode</param>
    /// <returns> Returns the next Card from the game mode specified in parameter</returns>
    ICard GetNextCard(GameMode gameMode);

    /// <summary>
    /// Returns a random card from any game mode and type
    /// </summary>
    /// <returns>Returns a random card from any game mode and type</returns>
    ICard GetRandomCard();

    /// <summary>
    /// List of all the cards in the database in any type or game mode
    /// </summary>
    /// <returns>Returns the list of all the cards in the database in any type or game mode</returns>
    IEnumerable<ICard> GetAllCards();

    /// <summary>
    /// Create default cards for the game
    /// </summary>
    /// <exception cref="SafeException">Thrown when TODO: ....</exception>
    void GenerateDefaultCards();

    /// <summary>
    /// Get all the cards in a specific game mode
    /// </summary>
    /// <param name="gameMode">specify the required game mode</param>
    /// <returns>Returns every cards in every type from a specified game mode</returns>
    IEnumerable<ICard> GetCardsByGameMode(GameMode gameMode);
}
