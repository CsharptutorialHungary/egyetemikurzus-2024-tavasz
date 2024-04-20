using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

using static System.Formats.Asn1.AsnWriter;

namespace CVBNMY
{
    internal static class PlayerScoreSerializer
    {

        private static readonly string _logFileName = "player_score.json";

        private static readonly string _logFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), _logFileName);
     
        public static void UpdatePlayerScoreJsonFile(string word, int score)
        {
            try
            {
                // Deserialize the existing JSON key-value pairs from the log file
                List<PlayerScore> scores = LoadScoresFromFile();

                // Refresh the key-value pairs
                scores.Add(new PlayerScore(word, score));

                // Sort the record in ABC-order and by scores in descending order using LINQ
                scores = scores.OrderBy(score => score.Word).ThenByDescending(score => score.Score).ToList();

                // Serialize the list of PlayerScores to JSON text
                string jsonText = JsonSerializer.Serialize(scores, new JsonSerializerOptions { WriteIndented = true });

                //Debug purpose only
                //Console.WriteLine(json);

                // Append the logfile 
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
                scores = JsonSerializer.Deserialize<List<PlayerScore>>(jsonText);
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
