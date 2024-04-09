using WHBNDL.Database;
using WHBNDL.Domain;
using WHBNDL.Infrastructure;

namespace WHBNDL.Application
{
    internal class StartCommand : IShellCommand
    {
        private readonly MemoryDatabase _database;

        public string Name => "start";

        public StartCommand(MemoryDatabase database)
        {
            _database = database;
        }

        public void Execute(IHost host, string[] args)
        {
            Question[] questions = QuestionsDeserializer.Deserialize();
            var quizManager = new QuizManager(questions, _database);
            quizManager.StartQuiz();
        }

    }
}
