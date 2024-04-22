using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace BFR0QN
{
    internal class BeolvasJson
    {
        public static void ReadJasonFile(string JsonFileName)
        {
            dynamic json =JsonConvert.DeserializeObject(File.ReadAllText(JsonFileName));
           
        }
    }
}
