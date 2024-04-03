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

        private int guesses;
       
        public HangmanGame(Difficulty difficulty)
        {
            words = LoadWordList(difficulty);
            SetGuesses(GuessesPerDifficulty[Convert.ToInt32(difficulty)]);
        }

        public List<Word> Words 
        {
            get { return words; }
        }
        public void SetGuesses(int newGuesses) 
        {
            if(newGuesses < 0)
            {
                throw new ArgumentException("Number of guessses cannot be negative.");
            }
            guesses = newGuesses;
        }

        private static List<Word> LoadWordList(Difficulty difficulty) 
        {
            int difficultyValue = Convert.ToInt32(difficulty);
            string[] strWords = WordLoader.ReadWords(WordLoader.WordFilePath(difficultyValue));

            List<Word> tempWords = new List<Word>();
            foreach (string strWord in strWords)
            {
                tempWords.Add(new Word(strWord, string.Concat(Enumerable.Repeat("_", strWord.Length))));
            }
            return tempWords;
        }

    }
}
