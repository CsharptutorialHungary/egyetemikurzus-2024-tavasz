using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

using XWG8AW.Domain;

namespace XWG8AW.Infrastructure
{
    internal class UserSerializer
    {
        public async void UseSerializeToJson()
        {
            string fullpath = Environment.CurrentDirectory;
            string path = fullpath.Substring(0, fullpath.Length - 16);
            string correctPath = string.Concat(path, "playersScores.json");

            try
            {
                UserDeserializer currentJson = new UserDeserializer();
                List<User> allUser = await currentJson.UserDeserializeFromJson();

                File.Delete(correctPath);

                User a = new User("asd");
                a.Score = 999;

                allUser.Add(a);

                using (var stream = File.OpenWrite(correctPath))
                {

                    JsonSerializer.Serialize(stream, allUser, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });


                }

            }
            catch (IOException ex)
            {
                Console.WriteLine("Hiba a játékosok kiírása során!");
            }
        }

    }
}
