using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CVBNMY
{
    internal static class PlayerScoreSerializer
    {
        private static void SerializeScoreListToJSON(Stream target, List<PlayerScore> instance)
        {
            JsonSerializer.Serialize(target, instance, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }

        private static void SerializeScoreToJSON(Stream target, PlayerScore instance)
        {
            JsonSerializer.Serialize(target, instance, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }


        public static void UpdatePlayerScoreJsonFile(string word, int score)
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "player_score.json");

            string currentJsonContent = File.ReadAllText(path);

            if (File.Exists(path))
            {
                using (var stream = File.Create(path))
                {
                    List<PlayerScore> scores = new List<PlayerScore>();
                    try
                    {
                        scores = JsonSerializer.Deserialize<List<PlayerScore>>(currentJsonContent);
                        JsonSerializer.Serialize(stream, scores, new JsonSerializerOptions
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                        });

                    }
                    catch (IOException)
                    {
                        Console.WriteLine("IOException at JSON-Serialization");
                    }
                    catch (JsonException)
                    {
                        scores = new List<PlayerScore>();
                    }

                    scores.Add(new PlayerScore(word, score));
                    SerializeScoreListToJSON(stream, scores);
                }
            }
            else
            {
                using (var stream = File.Create(path))
                {
                    SerializeScoreToJSON(stream, new PlayerScore(word, score));
                }
            }

        }
    }
}
