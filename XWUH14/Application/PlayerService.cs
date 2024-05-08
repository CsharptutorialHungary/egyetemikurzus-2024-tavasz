using XWUH14.Domain.Entities;

namespace XWUH14.Application
{
    public class PlayerService
    {
        private readonly List<Player> _players;

        public PlayerService(List<Player> players)
        {
            _players = players;
        }

        public void AddPlayer(Player player)
        {
            _players.Add(player);
        }
        public Player TopPlayer()
        {
            return _players.OrderByDescending(player => player.Score).FirstOrDefault();
        }

        public IEnumerable<Player> GetPlayers()
        {
            return _players;
        }
    }
}
