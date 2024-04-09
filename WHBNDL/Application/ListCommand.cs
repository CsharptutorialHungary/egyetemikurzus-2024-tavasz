using WHBNDL.Database;
using WHBNDL.Infrastructure;

namespace WHBNDL.Application
{
    internal class ListCommand : IShellCommand
    {
        private readonly MemoryDatabase _database;

        public string Name => "list";

        public ListCommand(MemoryDatabase database)
        {
            _database = database;
        }

        public async void Execute(IHost host, string[] args)
        {
            try
            {
                var results = await _database.ListQuizResultsAsync();
                foreach (var result in results)
                {
                    Console.WriteLine($"Correct Answers: {result.CorrectAnswers}, Total Questions: {result.TotalQuestions}, Timestamp: {result.Timestamp}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while listing quiz results: {ex.Message}");
            }
        }
    }
}