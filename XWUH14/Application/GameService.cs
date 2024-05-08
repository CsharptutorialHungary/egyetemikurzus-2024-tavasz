using XWUH14.Domain.Entities;
using XWUH14.Domain.Interfaces;

namespace XWUH14.Application
{
    public class GameService : IGameService
    {
        private readonly IQuestionProvider _questionProvider;
        private readonly IAnswerValidator _answerValidator;
        private readonly PlayerService _playerService;

        public GameService(IQuestionProvider questionProvider, IAnswerValidator answerValidator, PlayerService playerService)
        {
            _questionProvider = questionProvider;
            _answerValidator = answerValidator;
            _playerService = playerService;
        }

        public async Task StartGameAsync()
        {
            var allQuestions = await _questionProvider.GetQuestionsAsync();

            if (!allQuestions.Any())
            {
                Console.WriteLine("Nincsenek elérhető kérdések, így nem tudjuk a játékot elindítani.");
                return;
            }

            if(!_playerService.GetPlayers().Any())
            {
                Console.WriteLine("Nincsenek játékosok, így nem tudjuk a játékot elindítani.");
                return;
            }

            var gameQuestions = allQuestions.OrderBy(question => Guid.NewGuid()).Take(10).ToList();

            foreach (var player in _playerService.GetPlayers())
            {
                foreach (var question in gameQuestions)
                {
                    Console.WriteLine($"{player.Name}, {question.Text}");

                    var answer = Console.ReadLine();
                    Console.WriteLine("\n");

                    if (_answerValidator.ValidateAnswer(question, new Answer(answer)))
                    {
                        player.IncreaseScore(1);
                    }
                }
                Console.WriteLine("\n");
            }

            var winner = _playerService.TopPlayer();

            Console.WriteLine($"A nyertes {winner.Name}, pontja: {winner.Score}");
            return;
        }
    }
}
