using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace BFR0QN
{
    public class SaveTheGameToJson
    {
        public static void Save(Dictionary<string, int> saves)
        {
            var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "mentesek.json"); //C:\Users\Felhasználónév\AppData\Local\mentesek.json

            var saveJson = JsonSerializer.Serialize(saves, new JsonSerializerOptions(JsonSerializerDefaults.Web)
            {
                WriteIndented = true,
            });
            File.WriteAllText(filePath, saveJson);
        }
    }
}
