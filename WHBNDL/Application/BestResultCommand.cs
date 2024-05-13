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
                if (result.Message != null)
                {
                    Console.WriteLine(" ");
                    Console.WriteLine(result.Message);
                    Console.WriteLine(" ");
                }
                else
                {
                    Console.WriteLine($"\nBest result: Correct Answers: {result.CorrectAnswers}, Timestamp: {result.Timestamp}\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nAn error occurred while getting the best quiz result: {ex.Message}\n");
            }
        }

    }
}
