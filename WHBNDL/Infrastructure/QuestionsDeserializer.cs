using System.Text.Json;

using WHBNDL.Domain;

namespace WHBNDL.Infrastructure
{
    internal class QuestionsDeserializer
    {
        public static Question[] Deserialize()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\", "Resources", "questions.json");
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
