using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using E5G0RD.Domain;

namespace E5G0RD.Infrastructure
{
    public class WordRepository
    {

        public async Task<List<Word>> LoadWordsAsync(string fileName)
        {
            try
            {
                string rootDirectory = Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.Parent.FullName;
                string resourcesPath = Path.Combine(rootDirectory, "Resources");
                string filePath = Path.Combine(resourcesPath, fileName);

                var json = await File.ReadAllTextAsync(filePath);
                var words = JsonSerializer.Deserialize<List<Word>>(json);
                return words.Where(word => word.Value.Length == 5).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading words: {ex.Message}");
                return new List<Word>();
            }
        }

    }
}
