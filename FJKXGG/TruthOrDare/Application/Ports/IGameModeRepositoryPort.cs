using TruthOrDare.Domain.Entities;

namespace TruthOrDare.Application.Ports
{
    /// <summary>
    /// Outgoing port to get and manage game modes in the database
    /// </summary>
    internal interface IGameModeRepositoryPort
    {
        public GameMode GetGameModeById(int id);
        IEnumerable<GameMode> GetAllGameModes();
    }
}