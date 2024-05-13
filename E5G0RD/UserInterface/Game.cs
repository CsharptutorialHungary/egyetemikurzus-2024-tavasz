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
        private Word _secretWord;
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
            while (true)
            {
                Console.WriteLine("Welcome to the game!");
                Console.WriteLine("Guess the secret word with the least tries!");
                Console.WriteLine("(the secret word is 5 chars long and your guesses should be too)");
                Console.WriteLine();
                Console.WriteLine("_ indicates that the char is not in the secret word");
                Console.WriteLine("* means that the char is in the secret word, but in another position");
                Console.WriteLine();
                Console.WriteLine("By writing help, you recieve a hint");
                Console.WriteLine("By writing give up, the game ends and you get the secret word");
                Console.WriteLine();

                while (true)
                {
                    if (_guesses.Count() > 0)
                    {
                        Console.WriteLine($"Number of guesses so far: {_guesses.Count()}");
                    }

                    Console.Write("Your guess: ");
                    string word = Console.ReadLine();

                    if (word.ToLower() == "help")
                    {
                        Console.WriteLine($"Hint: {_secretWord.Hint}\n");
                        continue;
                    }

                    if (word.ToLower() == "give up")
                    {
                        Console.WriteLine($"You gave up! The secret word was: {_secretWord.Value}\n");
                        break;
                    }

                    string feedback = MakeGuess(word);

                    Console.WriteLine($"Result: {feedback}\n");

                    if (feedback.Equals(_secretWord.Value))
                    {
                        Console.WriteLine($"Congratulations, you guessed the secret word with {_guesses.Count()} guesses!");
                        break;
                    }
                }

                Console.Write("Press 'n' to start a new game or 'q' to quit: ");
                string input = Console.ReadLine().ToLower();

                if (input == "q")
                {
                    break;
                }
                else if (input == "n")
                {
                    _guesses.Clear();
                    _secretWord = GetRandomWord();
                }

                Console.WriteLine();

            }
        }

    }
}
