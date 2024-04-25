using TruthOrDare.Application.Ports;
using TruthOrDare.Domain.Entities;

namespace TruthOrDare.Application.Controllers;
internal class GameModeController : IGameModePort
{
    private readonly IGameModeRepositoryPort _gameModeDbPort;
    public GameModeController(IGameModeRepositoryPort gameModeDbPort)
    {
        _gameModeDbPort = gameModeDbPort;
    }

    public IEnumerable<GameMode> GetAllGameModes()
    {
        var gameModes = _gameModeDbPort.GetAllGameModes();
        return gameModes;
    }

    public GameMode GetGameModeById(int id)
    {
        throw new NotImplementedException();
    }
}
