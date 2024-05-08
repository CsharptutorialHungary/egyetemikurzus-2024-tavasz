using XWUH14.Application;
using XWUH14.Domain.Entities;
using XWUH14.Infrastructure;

namespace XWUH14.UserInterface
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var players = new List<Player>
            {
                new Player("Player 1"),
                new Player("Player 2")
            };

            var playerService = new PlayerService(players);
            var questionProvider = new FileQuestionProvider("Assets/questions.json");
            var answerValidator = new AnswerValidator();

            var gameService = new GameService(questionProvider, answerValidator, playerService);

            gameService.StartGameAsync().Wait();
            Console.WriteLine("Vége a játéknak.");

        }
    }
}