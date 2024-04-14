using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TruthOrDare.Application.Ports;
using TruthOrDare.Domain.Entities;

namespace TruthOrDare.Application.Controllers
{
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
            if (!gameModes.Any())
            {

            }
            return gameModes;
        }

        public GameMode GetGameModeById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
