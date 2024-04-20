using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CVBNMY;

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
        private const int EASY_GUESSES   = 7;

        private const int MEDIUM_GUESSES = 6;

        private const int HARD_GUESSES   = 5;

        private static readonly int[] GuessesPerDifficulty = { EASY_GUESSES, MEDIUM_GUESSES, HARD_GUESSES};

        private static readonly string[] difficultyTexts = { "Könnyű  (7 tippelési lehetőseg)", "Közepes (6 tippelési lehetőség)", "Nehéz   (5 tippelési lehetőség)" };

        private readonly List<Word> words;

        private readonly Word wordToGuess;

        private readonly int maxGuesses;

        public HangmanGame()
        {
            Difficulty difficulty = GetSelectedDifficulty();

            words = LoadWordList(difficulty);
            if(words.Count == 0)
            {
                throw new ListEmptyException("Unable to load words.");
            }
            wordToGuess = WordRandomizer.PickWord(words);
            maxGuesses = GuessesPerDifficulty[(int)(difficulty)];
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
                PlayerScoreSerializer.UpdatePlayerScoreJsonFile(WordToGuess.WholeWord, MaxGuesses - falseGuesses);
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

        private Difficulty GetSelectedDifficulty()
        {
      
            int selectedDifficulty = (int)Difficulty.EASY;

            ConsoleKeyInfo pressedKey;

            do
            {
                RenderClear();
                RenderDifficultyOptions((Difficulty)selectedDifficulty, difficultyTexts);

                pressedKey = Console.ReadKey(true);
                switch (pressedKey.Key)
                {
                    case ConsoleKey.UpArrow:
                        selectedDifficulty = (selectedDifficulty - 1 + Enum.GetValues(typeof(Difficulty)).Length) % Enum.GetValues(typeof(Difficulty)).Length;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedDifficulty = (selectedDifficulty + 1) % Enum.GetValues(typeof(Difficulty)).Length;
                        break;
                    case ConsoleKey.Enter:
                        RenderClear();
                        return (Difficulty)selectedDifficulty;
                }
            } while (true);
        }

        public void RenderGameState(string currentHiddenWord, List<char> characterGuesses, int remainingGuesses)
        {
            Console.WriteLine($"Jelenlegi állás: {currentHiddenWord}");
            Console.WriteLine($"Tippek: {string.Join(", ", characterGuesses)}");
            Console.WriteLine($"{remainingGuesses} tipp lehetőséged maradt.");
        }

        public void RenderDifficultyOptions(Difficulty selectedDifficulty, string[] optionTexts)
        {
            Console.WriteLine("Üdvözöllek az Akasztófa Játékban! Kérlek válassz nehézségi szintet.");
            Console.WriteLine();
            for (int i = 0; i < Enum.GetValues(typeof(Difficulty)).Length; i++)
            {
                string prefix = (i == (int)selectedDifficulty) ? "->" : "  ";
                Console.WriteLine($"{prefix}{optionTexts[i]}");
                Console.WriteLine();
            }
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
