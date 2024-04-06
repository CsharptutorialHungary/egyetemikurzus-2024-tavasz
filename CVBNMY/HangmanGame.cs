using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBNMY
{
    enum Difficulty
    {
        EASY = 0,
        MEDIUM,
        HARD
    }
    
    internal class HangmanGame
    {
        private const int EASY_GUESSES   =    7;

        private const int MEDIUM_GUESSES =    6;

        private const int HARD_GUESSES   =    5;

        private static readonly int[] GuessesPerDifficulty = { EASY_GUESSES, MEDIUM_GUESSES, HARD_GUESSES};

        private readonly List<Word> words;

        private readonly Word wordToGuess;

        private int maxGuesses;

        public HangmanGame(Difficulty difficulty)
        {

            words = LoadWordList(difficulty);
            if(words.Count == 0)
            {
                throw new ListEmptyException("Unable to load words.");
            }
            wordToGuess = WordRandomizer.PickWord(words);
            SetGuesses(GuessesPerDifficulty[Convert.ToInt32(difficulty)]);
        }

        public async Task HangmanGameTask()
        {
            var hiddenWord = string.Concat(Enumerable.Repeat("_", wordToGuess.WholeWord.Length));
            var characterGuesses = new List<char>();
            var falseGuesses = 0;

            while (falseGuesses < MaxGuesses && !hiddenWord.Equals(wordToGuess.WholeWord))
            {
                Console.WriteLine($"Jelenlegi állás: {hiddenWord}");
                Console.WriteLine($"Tippek: {string.Join(", ", characterGuesses)}");
                Console.WriteLine($"{MaxGuesses - falseGuesses} tipp lehetőséged maradt.");

                // Start a new task on different thread, read the input character asynchronously
                var inputTask = Task.Run(() => Console.ReadKey(false));
                var inputCharacter = await inputTask;

                if (characterGuesses.Contains(inputCharacter.KeyChar))
                {
                    Console.WriteLine("Már beírtad ezt a betűt!");
                    continue;
                }

                characterGuesses.Add(inputCharacter.KeyChar);

                // If the player has guessed a correct letter(even if it appears multiple time in the word), refresh the fields accordingly.
                if (WordToGuess.WholeWord.Contains(inputCharacter.KeyChar))
                {
                    var newHiddenWord = "";
                    for (var i = 0; i < WordToGuess.WholeWord.Length; i++)
                    {

                        if (WordToGuess.WholeWord[i] == inputCharacter.KeyChar)
                        {
                            newHiddenWord += inputCharacter.KeyChar;
                        }
                        else
                        {
                            newHiddenWord += hiddenWord[i];
                        }
                    }
                    hiddenWord = newHiddenWord;
                }
                // Otherwise decrement the number of the remaining guesses
                else
                {
                    falseGuesses++;
                }
                Console.WriteLine();
            }

            if (hiddenWord.Equals(WordToGuess.WholeWord))
            {
                Console.WriteLine("Gratulálok, kitaláltad a szót!");
            }
            else
            {
                Console.WriteLine("Sajnos nem sikerült kitalálnod a szót. A szó: {0}", WordToGuess.WholeWord);
            }
        }

        public List<Word> Words 
        {
            get { return words; }
        }
        private void SetGuesses(int newGuesses) 
        {
            if(newGuesses < 0)
            {
                throw new ArgumentException("Number of guessses cannot be negative.");
            }
            maxGuesses = newGuesses;
        }

        public int MaxGuesses
        {
            get { return maxGuesses; }
        }

        public Word WordToGuess
        {
             get { return wordToGuess; }
        }

        private static List<Word> LoadWordList(Difficulty difficulty) 
        {
            int difficultyValue = Convert.ToInt32(difficulty);
            string[] strWords;
            strWords = WordLoader.ReadWords(WordLoader.WordFilePath(difficultyValue));
            List<Word> tempWords = new List<Word>();
            foreach (string strWord in strWords)
            {
                tempWords.Add(new Word(strWord, string.Concat(Enumerable.Repeat("_", strWord.Length))));
            }
            return tempWords;
        }

        public override string ToString()
        {
            string wordList = string.Join(", ", words.Select(word => word.WholeWord));
            return $"Number of guesses: {MaxGuesses}\nWords: {wordList}\nRandomized word: {wordToGuess.WholeWord}";
        }
    }
}
