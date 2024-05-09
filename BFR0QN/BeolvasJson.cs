using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.Json;

using BFR0QN.Etelek;

namespace BFR0QN
{
    public static class BeolvasJson
    {
        public static List<Hamburger> ReadJsonFile(string jsonFileName)
        {
            List<Hamburger> burgers = new List<Hamburger>();
            try
            {
                string jsonText = File.ReadAllText(jsonFileName);
                burgers = JsonSerializer.Deserialize<List<Hamburger>>(jsonText,new JsonSerializerOptions(JsonSerializerDefaults.Web));
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
            return burgers;
        }
        public static async Task<Dictionary<string, int>> Betolt()
        {
            var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "mentesek.json");
            if (!File.Exists(filePath))
            {
                return new Dictionary<string, int>();
            }
            var mentesekJson = await File.ReadAllTextAsync(filePath);
            Dictionary<string, int> mentesek = JsonSerializer.Deserialize<Dictionary<string, int>>(mentesekJson);
            return mentesek;
        }
    }
}