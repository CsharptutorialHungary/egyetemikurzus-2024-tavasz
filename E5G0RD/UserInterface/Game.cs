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
        private readonly Word _secretWord;
        private readonly List<Guess> _guesses;

        public Game(List<Word> words)
        {
            _words = words;
            _secretWord = GetRandomWord();
            _guesses = new List<Guess>();
        }

        private Word GetRandomWord()
        {
            var random = new Random();
            int index = random.Next(_words.Count);
            return _words[index];
        }

        public string MakeGuess(string word)
        {
            if (word.Length != 5)
            {
                   return "Your guess should be exactly 5 characters long!";
            }

            StringBuilder feedback = new StringBuilder();

            for (int i = 0; i < word.Length; i++)
            {
                if (word[i] == _secretWord.Value[i])
                {
                    feedback.Append(word[i]);
                }
                else if (_secretWord.Value.Contains(word[i]))
                {
                    feedback.Append('*');
                }
                else
                {
                    feedback.Append('_');
                }
            }

            var guess = new Guess(word, feedback.ToString());
            _guesses.Add(guess);
            return feedback.ToString();
        }

        public void Start()
        {
            Console.WriteLine("Welcome to the game!");
            Console.WriteLine("Guess the secret word with the least tries!");
            Console.WriteLine("(the secret word is 5 chars long and your guesses should be too)");
            Console.WriteLine();

            while (true)
            {
                if (_guesses.Count() > 0)
                {
                    Console.WriteLine($"Number of guesses so far: {_guesses.Count()}");
                }

                Console.Write("Your guess: ");
                string word = Console.ReadLine();
                string feedback = MakeGuess(word);

                Console.WriteLine($"Result: {feedback}\n");

                if (feedback.Equals(_secretWord.Value))
                {
                    Console.WriteLine($"Congratulations, you guessed the secret word with {_guesses.Count()} guesses!");
                    break;
                }
            }
        }

    }
}
