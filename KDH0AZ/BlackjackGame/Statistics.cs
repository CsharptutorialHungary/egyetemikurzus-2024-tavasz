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
            Console.WriteLine("Nincs megjelen�thet� adat. A data.json f�jl nem l�tezik.");
            return;
        }

        try
        {
            string jsonData = File.ReadAllText(FilePath);

            if (string.IsNullOrWhiteSpace(jsonData))
            {
                Console.WriteLine("M�g nem j�tszottak a j�t�kkal :'(");
                return;
            }

            var playerDataList = JsonSerializer.Deserialize<List<PlayerData>>(jsonData);

            if (playerDataList == null || playerDataList.Count == 0)
            {
                Console.WriteLine("M�g nem j�tszottak a j�t�kkal :'(");
                return;
            }

            Console.WriteLine("TOP 5 Legt�bb p�nzzel rendelkez� j�t�kosok:");

            var sortedPlayers = playerDataList.OrderByDescending(p => p.Money).Take(count);

            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine("|            N�v            |         P�nz�sszeg        |");
            Console.WriteLine("--------------------------------------------------------");

            foreach (var player in sortedPlayers)
            {
                Console.WriteLine($"| {player.Name,-25} | ${player.Money,-24} |");
            }

            Console.WriteLine("--------------------------------------------------------");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("A data.json f�jl nem tal�lhat�.");
        }
        catch (JsonException)
        {
            Console.WriteLine("Hiba t�rt�nt az adatok olvas�sa sor�n. A f�jl tartalma nem megfelel� JSON form�tum�.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Hiba t�rt�nt az adatok bet�lt�sekor: {ex.Message}");
        }
    }

    private class PlayerData
    {
        public string Name { get; set; }
        public int Money { get; set; }
    }
}
