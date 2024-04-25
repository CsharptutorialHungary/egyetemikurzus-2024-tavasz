using TruthOrDare.Application.Ports;
using TruthOrDare.Domain.Entities;
using TruthOrDare.Domain.Exceptions;

namespace TruthOrDare.Infrastructure.Repositories;

// TODO: Violates the Single Responsibility Principle
// TODO: Violates Repository Pattern
// DONE: Violates YAGNI
internal class GameModeRepository : IGameModeRepositoryPort
{
    private readonly List<GameMode> _gameModes = [
        new GameMode
        {
            Id = 0,
            Name = "Friends",
            Description = "The classic game mode."
        },
        new GameMode
        {
            Id = 1,
            Name = "Party",
            Description = "A new drinking game."
        },
        new GameMode
        {
            Id = 2,
            Name = "Romantic",
            Description = "Just for couples."
        }
        ];

    public IEnumerable<GameMode> GetAllGameModes() => _gameModes;
    public GameMode GetGameModeById(int id) => _gameModes.FirstOrDefault(gm => gm.Id == id) ?? throw new SafeException("Game mode not found.");
}
