using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

using CVBNMY.Domain;


namespace CVBNMY.Infrastructure
{
    /// <summary>
    /// This class provides static methods for serializing and deserializing player scores to a JSON file.
    /// </summary>
    internal static class PlayerScoreSerializer
    {

        private static readonly string _logFileName = "player_score.json";

        private static readonly string _logFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), _logFileName);

        /// <summary>
        /// This function updates the "player_score.txt" file. In case the player wins, his result will be logged into the previous file.
        /// The results are sorted alphabetticaly (by the words), then by the reached points.
        /// </summary>
        public static void UpdatePlayerScoreJsonFile(string word, int score)
        {
            try
            {
                List<PlayerScore> scores = LoadScoresFromFile();
                scores.Add(new PlayerScore(word, score));
                scores = scores.OrderBy(score => score.Word).ThenByDescending(score => score.Score).ToList();
                string jsonText = JsonSerializer.Serialize(scores, new JsonSerializerOptions { WriteIndented = true });
                WriteToJSONFile(jsonText);

            }
            catch (IOException ex)
            {
                Console.WriteLine($"An error occurred while writing to log file: {ex.Message}");
            }
        }

        private static List<PlayerScore> LoadScoresFromFile()
        {
            List<PlayerScore> scores;

            if (File.Exists(_logFilePath))
            {
                string jsonText = File.ReadAllText(_logFilePath);
                try
                {
                    scores = JsonSerializer.Deserialize<List<PlayerScore>>(jsonText);
                }
                catch (JsonException)
                {
                    // JsonException is able to occur if the file exists, but it's empty
                    scores = new List<PlayerScore>();
                }
            }
            else
            {
                scores = new List<PlayerScore>();
            }

            return scores;
        }

        private static void WriteToJSONFile(string json)
        {
            File.WriteAllText(_logFilePath, json);
        }



    }
}
