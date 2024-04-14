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
    
    internal class HangmanGame : IRenderer
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
            var hiddenWord = wordToGuess.GuessedCharacters;
            var characterGuesses = new List<char>();
            var falseGuesses = 0;

            while (falseGuesses < MaxGuesses && !hiddenWord.Equals(wordToGuess.WholeWord))
            {
                RenderGameState(hiddenWord, characterGuesses, MaxGuesses - falseGuesses);

                char inputCharacter = await GetValidLetterInputAsync();

                if (characterGuesses.Contains(inputCharacter))
                {
                    RenderClear();
                    continue;
                }
                characterGuesses.Add(inputCharacter);

                // If the player has guessed a correct letter(even if it appears multiple time in the word), refresh the fields accordingly.
                if (WordToGuess.WholeWord.Contains(inputCharacter))
                {
                    var newHiddenWord = "";
                    for (var i = 0; i < WordToGuess.WholeWord.Length; i++)
                    {

                        if (WordToGuess.WholeWord[i] == inputCharacter)
                        {
                            newHiddenWord += inputCharacter;
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
                RenderClear();
            }

            if (hiddenWord.Equals(WordToGuess.WholeWord))
            {
                RenderGameState(hiddenWord, characterGuesses, MaxGuesses - falseGuesses);
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

        // This task is used to wait for the user input asynchronously separately, without blocking
        private static async Task<char> GetValidLetterInputAsync()
        {
            char inputCharacter;
            do
            {
                using (var inputTask = Task.Run(() => Console.ReadKey()))
                {
                    var inputKey = await inputTask;
                    if (char.IsLetter(inputKey.KeyChar))
                    {
                        inputCharacter = char.IsUpper(inputKey.KeyChar) ? char.ToLower(inputKey.KeyChar) : inputKey.KeyChar;
                        return inputCharacter;
                    }
                }
               
            } while (true);
        }

        public void RenderGameState(string currentHiddenWord, List<char> characterGuesses, int remainingGuesses)
        {
            Console.WriteLine($"Jelenlegi állás: {currentHiddenWord}");
            Console.WriteLine($"Tippek: {string.Join(", ", characterGuesses)}");
            Console.WriteLine($"{remainingGuesses} tipp lehetőséged maradt.");
        }

        public void RenderClear()
        {
            Console.Clear();
        }

        public override string ToString()
        {
            string wordList = string.Join(", ", words.Select(word => word.WholeWord));
            return $"Number of guesses: {MaxGuesses}\nWords: {wordList}\nRandomized word: {wordToGuess.WholeWord}\nWord strucutre: {wordToGuess.GuessedCharacters}";
        }


    }
}
