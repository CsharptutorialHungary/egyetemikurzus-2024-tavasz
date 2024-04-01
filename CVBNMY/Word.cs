using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBNMY
{
    internal record Word
    {
        private string _wholeWord;

        private string _guessedCharacters;

        public Word(string wholeWord, string guessedCharacters)
        {
            _wholeWord = wholeWord;
            _guessedCharacters = guessedCharacters;
        }

        public string WholeWord
        {
            get { return _wholeWord; }
        }

        public void SetWholeWord(string wholeWord)
        {
            _wholeWord = wholeWord;
        }

        public string GuessedCharacters
        {
            get { return _guessedCharacters; }
        }

        public void SetGuessedCharacters(string guessedCharacters)
        {
            _guessedCharacters = guessedCharacters;
        }
    }
}
