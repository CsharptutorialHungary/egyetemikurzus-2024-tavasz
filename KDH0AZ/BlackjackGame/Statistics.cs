using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class Statistics
{
    private const string FilePath = "F:\\Alkfejlesztes charp\\KDH0AZ\\BlackjackGame\\data.json";

    public void DisplayTopPlayersTable(int count)
    {
        if (!File.Exists(FilePath))
        {
            Console.WriteLine("Nincs megjeleníthetõ adat. A data.json fájl nem létezik.");
            return;
        }

        try
        {
            string jsonData = File.ReadAllText(FilePath);

            if (string.IsNullOrWhiteSpace(jsonData))
            {
                Console.WriteLine("Még nem játszottak a játékkal :'(");
                return;
            }

            var playerDataList = JsonSerializer.Deserialize<List<PlayerData>>(jsonData);

            if (playerDataList == null || playerDataList.Count == 0)
            {
                Console.WriteLine("Még nem játszottak a játékkal :'(");
                return;
            }

            Console.WriteLine("TOP 5 Legtöbb pénzzel rendelkezõ játékosok:");

            var sortedPlayers = playerDataList.OrderByDescending(p => p.Money).Take(count);

            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine("|            Név            |         Pénzösszeg        |");
            Console.WriteLine("--------------------------------------------------------");

            foreach (var player in sortedPlayers)
            {
                Console.WriteLine($"| {player.Name,-25} | ${player.Money,-24} |");
            }

            Console.WriteLine("--------------------------------------------------------");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("A data.json fájl nem található.");
        }
        catch (JsonException)
        {
            Console.WriteLine("Hiba történt az adatok olvasása során. A fájl tartalma nem megfelelõ JSON formátumú.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Hiba történt az adatok betöltésekor: {ex.Message}");
        }
    }

    private class PlayerData
    {
        public string Name { get; set; }
        public int Money { get; set; }
    }
}
