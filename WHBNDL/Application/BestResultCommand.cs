using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WHBNDL.Database;
using WHBNDL.Infrastructure;

namespace WHBNDL.Application
{
    internal class BestResultCommand : IShellCommand
    {
        private readonly MemoryDatabase _database;

        public string Name => "bestresult";

        public BestResultCommand(MemoryDatabase database)
        {
            _database = database;
        }

        public async void Execute(IHost host, string[] args)
        {
            try
            {
                var result = await _database.GetBestQuizResultAsync();
                Console.WriteLine($"Best result: Correct Answers: {result.CorrectAnswers}, Total Questions: {result.TotalQuestions}, Timestamp: {result.Timestamp}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while getting the best quiz result: {ex.Message}");
            }
        }
    }
}
