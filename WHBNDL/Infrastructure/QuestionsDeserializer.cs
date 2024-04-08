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
            return JsonSerializer.Deserialize<Question[]>(reader.ReadToEnd(), options);
        }
    }
}
