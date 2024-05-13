using System.Text.Json;

using WHBNDL.Domain;

namespace WHBNDL.Infrastructure
{
    internal class AppTextsDeserializer
    {
        public static Texts Deserialize()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\", "Resources", "appTexts.json");

            string jsonString = File.ReadAllText(path);
            TextReader reader = new StringReader(jsonString);
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            Texts appTexts = JsonSerializer.Deserialize<Texts>(reader.ReadToEnd(), options);
            return appTexts;
        }
    }
}
