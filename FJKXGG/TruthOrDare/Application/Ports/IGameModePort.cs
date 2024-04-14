using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TruthOrDare.Domain.Entities;

namespace TruthOrDare.Application.Ports
{
    /// <summary>
    /// Incoming port to get and manage game modes
    /// </summary>
    public interface IGameModePort
    {
        public IEnumerable<GameMode> GetAllGameModes();
        public GameMode GetGameModeById(int id);
    }
}
