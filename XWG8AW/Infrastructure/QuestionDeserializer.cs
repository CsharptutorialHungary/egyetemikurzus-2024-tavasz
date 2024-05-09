using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using XWG8AW.Domain;

namespace XWG8AW.Infrastructure
{
    internal class QuestionDeserializer
    {
        public async Task<Question[]> QuestionDeserializeFromJson()
        {
            string fullpath = Environment.CurrentDirectory;
            string path = fullpath.Substring(0, fullpath.Length - 16);
            string correctPath = string.Concat(path, "questions.json");

            try {
                using (var stream = File.OpenRead(correctPath))
                {
                    Question[]? questions = await JsonSerializer.DeserializeAsync<Question[]>(stream, new JsonSerializerOptions
                    {
                        AllowTrailingCommas = true,
                    });
                    if (questions is null)
                    {
                        Console.WriteLine("Deszerializációs hiba");
                        return null;
                    }
                    /*foreach (var question in questions)
                    {
                        Console.WriteLine(question);
                    }*/

                    return questions;
                }

            } catch (IOException ex) { 
                Console.WriteLine("Hiba a kérdések beolvasás során!");
                return null;
            }
        }
    }
}
