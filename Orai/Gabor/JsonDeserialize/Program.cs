using System.Text.Json;
using System.Text.Json.Serialization;

namespace JsonDeserialize
{
    //immutable típus amit a JSON fájlban tárolunk
    //A record itt leginkább a generált ToString() metódus miatt hasznos
    public sealed record class User
    {
        //JSON fájlban a "user_name" kulcs értéke
        //Annotálunk, mert nem camel case vagy pascal case a kulcs név séma
        [JsonPropertyName("user_name")]
        public required string UserName { get; init; }
        [JsonPropertyName("salary")]
        public required decimal Sallary { get; init; }
        [JsonPropertyName("work_start_time")]
        public required TimeOnly WorkStartTime { get; init; } //TimeOnly típus a .NET 6 óta elérhető
        [JsonPropertyName("work_end_time")]
        public required TimeOnly WorkEndTime { get; init; } //Csak időt tárol, dátumot nem.
        [JsonPropertyName("experience_years")]
        public required int ExperienceInYears { get; init; }
    }

    internal class Program
    {
        //Async main
        static async Task Main(string[] args)
        {
            using (var stream = File.OpenRead("data.json"))
            {
                //A fájl user tömböt tárol
                //A deszerializáció eredménye lehet null, amikor üres a fájl pl.
                User[]? users = await JsonSerializer.DeserializeAsync<User[]>(stream, new JsonSerializerOptions
                {
                    AllowTrailingCommas = true, //megengedjük, hogy vesszővel záródjon a tömb
                    NumberHandling = JsonNumberHandling.AllowReadingFromString //számokat stringként olvassuk
                });
                if (users is null) //pattern matching
                {
                    Console.WriteLine("Deszerializációs hiba");
                    return;
                }
                foreach (var user in users)
                {
                    Console.WriteLine(user);
                }
            }
        }
    }
}
