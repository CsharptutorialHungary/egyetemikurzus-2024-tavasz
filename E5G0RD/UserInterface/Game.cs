using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using E5G0RD.Domain;

namespace E5G0RD.UserInterface
{
    public class Game
    {
        private readonly List<Word> _words;

        public Game(List<Word> words)
        {
            _words = words;
        }

        public void Start()
        {
            Console.WriteLine("Welcome to the game!");
            Console.WriteLine("Press any key to start...");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("Game started!");
            Console.WriteLine("Press any key to end the game...");
            Console.ReadKey();
            Console.WriteLine("Game ended!");
        }

    }
}
