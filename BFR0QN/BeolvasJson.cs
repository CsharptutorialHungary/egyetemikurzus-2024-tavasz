using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BFR0QN.Burger;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace BFR0QN
{
    internal class BeolvasJson
    {
        public static List<Hamburger> ReadJsonFile(string jsonFileName)
        {
            List<Hamburger> burgers = new List<Hamburger>();
            try
            {
                string jsonText = File.ReadAllText(jsonFileName);
                burgers = JsonConvert.DeserializeObject<List<Hamburger>>(jsonText);
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"A fájl nem található: {ex.Message}");
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Hiba történt a JSON feldolgozása során: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba történt: {ex.Message}");
            }
            return burgers;
        }
    }
}
