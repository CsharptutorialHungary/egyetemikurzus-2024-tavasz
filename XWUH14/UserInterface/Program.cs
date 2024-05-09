using XWUH14.Application;
using XWUH14.Domain.Entities;
using XWUH14.Infrastructure;

namespace XWUH14.UserInterface
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.Write("Add meg hány ember szeretne játszani: ");
            int playerCount = int.Parse(Console.ReadLine());

            var players = new List<Player>();

            for (int i = 0; i < playerCount; i++)
            {
                Console.Write($"Add meg a {i + 1}. játékos nevét: ");
                string playerName = Console.ReadLine();
                players.Add(new Player(playerName));
            }

            Console.WriteLine("\n");
            Console.WriteLine("A válaszokat kérlek ékezetek nélkül add meg! Nem számít hogy kis/nagybetűt használsz.");
            Console.WriteLine("\n");

            var playerService = new PlayerService(players);
            var questionProvider = new FileQuestionProvider("Assets/questions.json");
            var answerValidator = new AnswerValidator();

            var gameService = new GameService(questionProvider, answerValidator, playerService);

            await gameService.StartGameAsync();
            Console.WriteLine("Vége a játéknak.");
        }
    }
}