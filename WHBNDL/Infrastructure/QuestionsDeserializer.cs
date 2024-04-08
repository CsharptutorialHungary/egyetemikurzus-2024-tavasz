using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using WHBNDL.Domain;

namespace WHBNDL.Infrastructure
{
    internal class QuestionsDeserializer
    {
        public static Question[] Deserialize()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "questions.json");
            string jsonString = File.ReadAllText(path);
            TextReader reader = new StringReader(jsonString);
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            Question[] questions = JsonSerializer.Deserialize<Question[]>(reader.ReadToEnd(), options);

            Random random = new Random();
            questions = questions.OrderBy(x => random.Next()).ToArray();

            questions = questions.Take(10).ToArray();

            return questions;
        }
    }
}
