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
    internal class UserDeserialize
    {
        public async Task<User[]> UserDeserializeFromJson()
        {
            string fullpath = Environment.CurrentDirectory;
            string path = fullpath.Substring(0, fullpath.Length - 16);
            string correctPath = string.Concat(path, "playersScores.json");

            try
            {
                using (var stream = File.OpenRead(correctPath))
                {

                    User[]? users = await JsonSerializer.DeserializeAsync<User[]>(stream, new JsonSerializerOptions
                    {
                        AllowTrailingCommas = true,
                        NumberHandling = JsonNumberHandling.AllowReadingFromString

                    });
                    if (users is null)
                    {
                        Console.WriteLine("Deszerializációs hiba");
                        return null;
                    }
                    /*foreach (var user in users)
                    {
                        Console.WriteLine(user);
                    }*/

                    return users;
                }

            }
            catch (IOException ex)
            {
                Console.WriteLine("Hiba a játékosok beolvasás során!");
                return null;
            }
        }
    }
}
