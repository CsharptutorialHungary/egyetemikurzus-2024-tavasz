using System;

namespace CVBNMY
{
    class Program
    {
        public static void Main(string[] args)
        {
            WordLoader wl = new WordLoader();
            string[] words = wl.ReadWords("C:\\Users\\Teodor\\Desktop\\egyetemikurzus-2024-tavasz\\CVBNMY\\WordFiles\\EasyWords.txt");
        }
    }
}