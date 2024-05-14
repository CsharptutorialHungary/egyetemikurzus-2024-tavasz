using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.Json;

namespace BFR0QN
{
    public static class ReadJsonFile
    {
        public static List<Food> ReadJsonFileToList(string jsonFileName)
        {
            List<Food> foods = new List<Food>();
            try
            {
                string jsonText = File.ReadAllText(jsonFileName);
                foods = JsonSerializer.Deserialize<List<Food>>(jsonText,new JsonSerializerOptions(JsonSerializerDefaults.Web));
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"A fájl nem található: {ex.Message}");
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Hiba történt a JSON feldolgozása során: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba történt: {ex.Message}");
            }
            return foods;
        }
        public static async Task<Dictionary<string, int>> LoadTheGame()
        {
            var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "mentesek.json");
            if (!File.Exists(filePath))
            {
                return new Dictionary<string, int>();
            }
            var savesInJson = await File.ReadAllTextAsync(filePath);
            Dictionary<string, int> convertTheSavesInJsonToDictionary = JsonSerializer.Deserialize<Dictionary<string, int>>(savesInJson);
            return convertTheSavesInJsonToDictionary;
        }
    }
}