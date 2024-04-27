using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace BFR0QN
{
    public class MentesJson
    {
        public static void Mentes(Dictionary<string, int> mentesek)
        {
            var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "mentesek.json");

            var mentesekJson = JsonSerializer.Serialize(mentesek, new JsonSerializerOptions(JsonSerializerDefaults.Web)
            {
                WriteIndented = true,
            });
            File.WriteAllText(filePath, mentesekJson);
        }
    }
}
