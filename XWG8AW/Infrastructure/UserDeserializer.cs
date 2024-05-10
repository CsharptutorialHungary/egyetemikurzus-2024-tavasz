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
    internal class UserDeserializer
    {
        public async Task<List<User>> UserDeserializeFromJson()
        {
            string fullpath = Environment.CurrentDirectory;
            string path = fullpath.Substring(0, fullpath.Length - 16);
            string correctPath = string.Concat(path, "playersScores.json");

            try
            {
                using (var stream = File.OpenRead(correctPath))
                {

                    List<UserJson>? users = await JsonSerializer.DeserializeAsync<List<UserJson>>(stream, new JsonSerializerOptions
                    {
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

                    List<User> allUser = new List<User>();

                    foreach (UserJson userJson in users)
                    {
                        allUser.Add(new User(userJson.UserName, userJson.Score));
                    }

                    return allUser;
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
