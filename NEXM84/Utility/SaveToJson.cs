using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using NEXM84.Infrastructure;
using NEXM84.Model;

namespace NEXM84.Utility
{
    internal class SaveToJson(List<Credential> credentials, string newFilePath)
    {
        public static async Task serialize(List<Credential> credentials, string saveLocation)
        {
            string json = await ConvertToJsonAsync(credentials);

            saveTheFile(json, saveLocation);
        }

        private static async Task<string> ConvertToJsonAsync(List<Credential> credentials)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                await Task.Run(() =>
                {
                    JsonSerializer.Serialize(memoryStream, credentials);
                });
                Console.WriteLine("Converted to json format...\n");
                return Encoding.UTF8.GetString(memoryStream.ToArray());
            }
        }

        private static async Task<bool> saveTheFile(string json, string saveLocation)
        {
            try
            {
                string filePath = Path.Combine(saveLocation, GenerateFileName());
                await File.WriteAllTextAsync(filePath, json);
                Console.WriteLine($"Successful save! ["+filePath+"]");
                return true;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"An error occurred while saving the file: {ex.Message}");
                return false;
            }
        }


        public static string GenerateFileName()
        {
            Random random = new Random();
            int randomNumber = random.Next(100, 1000);
            string fileName = $"{randomNumber}_pw.json";
            return fileName;
        }

    }
}
