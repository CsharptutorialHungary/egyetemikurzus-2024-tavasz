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

        public async Task<List<Word>> LoadWordsAsync(string path)
        {
            try
            {
                var json = await File.ReadAllTextAsync(path);
                var words = JsonSerializer.Deserialize<List<Word>>(json);
                return words;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading words: {ex.Message}");
                return new List<Word>();
            }
        }

    }
}
