using TruthOrDare.Domain.Entities;

// TODO: document exceptions

namespace TruthOrDare.Application.Ports;

/// <summary>
/// Outgoing port to get and manage game modes in the database. The database module can implement this.
/// </summary>
internal interface IGameModeRepositoryPort
{
    /// <summary>
    /// Get all the game modes in the database
    /// </summary>
    /// <returns>The list of all game modes</returns>
    /// <exception cref="SafeException">Thrown when there are ...</exception>"
    IEnumerable<GameMode> GetAllGameModes();

    /// <summary>
    /// Get a game mode by its id
    /// </summary>
    /// <param name="id">identifier number of the needed GameMode</param>
    /// <returns>The needed GameMode object</returns>
    /// <exception cref="SafeException">Thrown when the game mode with the specified id is not found</exception>"
    public GameMode GetGameModeById(int id);
}