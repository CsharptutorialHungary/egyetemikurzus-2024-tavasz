using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBNMY.Infrastructure
{
    internal static class WordRandomizer
    {
        public static string PickWord(List<string> words)
        {
            Random r = new Random();
            int index = r.Next(words.Count);
            return words[index];
        }
    }
}
