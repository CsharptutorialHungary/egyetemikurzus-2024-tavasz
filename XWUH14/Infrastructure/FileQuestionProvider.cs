using System.Text.Json;
using XWUH14.Domain.Entities;
using XWUH14.Domain.Interfaces;

namespace XWUH14.Infrastructure
{
    public class FileQuestionProvider : IQuestionProvider
    {
        private readonly string _filePath;

        public FileQuestionProvider(string filePath)
        {
            _filePath = filePath;
        }

        public async Task<IEnumerable<Question>> GetQuestionsAsync()
        {
            try
            {
                var json = await File.ReadAllTextAsync(_filePath);
                var questions = JsonSerializer.Deserialize<List<Question>>(json);
                return questions ?? new List<Question>();
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Nem letezik a megadott fajl: {ex.Message}");
                return new List<Question>();
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Problema tortent a json fajl deszerializaciojaval: {ex.Message}");
                return new List<Question>();
            }

        }
    }
}
