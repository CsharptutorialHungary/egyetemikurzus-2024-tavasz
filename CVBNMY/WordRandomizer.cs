using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBNMY
{
    internal static class WordRandomizer
    {
        public static string PickWord(string[] words)
        {
            Random r = new Random();
            int index = r.Next(words.Length);
            return words[index];
        }
    }
}
