using TruthOrDare.Domain.Entities;

// TODO: document exceptions

namespace TruthOrDare.Application.Ports;

/// <summary>
/// Incoming port to get and manage game modes. The user interface or the REST API module cloud use this port to get GameModes.
/// </summary>
public interface IGameModePort
{
    /// <summary>
    /// Get all the game modes in the database
    /// </summary>
    /// <returns>The list of all game modes</returns>
    public IEnumerable<GameMode> GetAllGameModes();

    /// <summary>
    /// Get a game mode by its id
    /// </summary>
    /// <param name="id">identifier number of the needed GameMode</param>
    /// <returns>The GameMode object</returns>
    /// <exception cref="SafeException">Thrown when the game mode with the specified id is not found</exception>"
    public GameMode GetGameModeById(int id);
}
