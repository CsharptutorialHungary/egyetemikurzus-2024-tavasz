using System;

namespace CVBNMY
{
    class Program
    {
        public static void Main(string[] args)
        { 
            WordLoader.ReadWords(WordLoader.WordFilePath(0));
        }
    }
}