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
                var groupedResults = await _database.ListQuizResultsAsync();
                if (groupedResults.Count == 0)
                {
                    Console.WriteLine("\nNo quiz results found.\n");
                }
                else
                {
                    Console.WriteLine();
                    foreach (var group in groupedResults)
                    {
                        Console.WriteLine($"Correct Answers: {group.Key}");
                        foreach (var result in group.Value)
                        {
                            Console.WriteLine($" Timestamp: {result.Timestamp}");
                        }
                    }
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nAn error occurred while listing quiz results: {ex.Message}\n");
            }
        }
    }
}