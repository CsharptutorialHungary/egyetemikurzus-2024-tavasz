using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBNMY
{
    internal static class WordRandomizer
    {
        public static Word PickWord(List<Word> words)
        {
            Random r = new Random();
            int index = r.Next(words.Count);
            return words[index];
        }
    }
}
