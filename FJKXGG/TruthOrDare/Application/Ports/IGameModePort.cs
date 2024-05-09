using TruthOrDare.Domain.Entities;

// TODO: document exceptions

namespace TruthOrDare.Application.Ports;

// DONE: Violates YAGNO principle

/// <summary>
/// Incoming port to get and manage game modes. The user interface or the REST API module cloud use this port to get GameModes.
/// </summary>
public interface IGameModePort
{
    /// <summary>
    /// Get all the game modes in the database
    /// </summary>
    /// <returns>The list of all game modes</returns>
    IEnumerable<GameMode> GetAllGameModes();
}
