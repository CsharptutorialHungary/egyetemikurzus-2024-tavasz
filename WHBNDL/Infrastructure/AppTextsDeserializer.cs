using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using WHBNDL.Domain;

namespace WHBNDL.Infrastructure
{
    internal class AppTextsDeserializer
    {
        public static Texts Deserialize()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "appTexts.json");
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
