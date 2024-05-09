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
    internal class QuestionDeserialize
    {
        public async Task<Question[]> QuestionDeserializeFromJson()
        {
            try {
                using (var stream = File.OpenRead("questions.json"))
                {
                    Question[]? questions = await JsonSerializer.DeserializeAsync<Question[]>(stream, new JsonSerializerOptions
                    {
                        AllowTrailingCommas = true, //megengedjük, hogy vesszővel záródjon a tömb
                    });
                    if (questions is null)
                    {
                        Console.WriteLine("Deszerializációs hiba");
                        return null;
                    }
                    foreach (var question in questions)
                    {
                        Console.WriteLine(question);
                    }

                    return questions;
                }

            } catch (IOException ex) { 
                Console.WriteLine("Hiba a kérdések beolvasás során!");
                return null;
            }
        }
    }
}
