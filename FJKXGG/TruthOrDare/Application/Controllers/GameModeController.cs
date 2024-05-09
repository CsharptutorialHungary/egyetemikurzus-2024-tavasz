using TruthOrDare.Application.Ports;
using TruthOrDare.Domain.Entities;

namespace TruthOrDare.Application.Controllers;
internal class GameModeController(IGameModeRepositoryPort gameModeDbPort) : IGameModePort
{
    private readonly IGameModeRepositoryPort _gameModeDbPort = gameModeDbPort;

    public IEnumerable<GameMode> GetAllGameModes()
    {
        return _gameModeDbPort.GetAllGameModes();
    }
}
