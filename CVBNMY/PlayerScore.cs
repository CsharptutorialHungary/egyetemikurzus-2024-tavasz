using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBNMY
{
    internal sealed record PlayerScore
    {
        private string _word;

        private int _score;

        public PlayerScore(string word, int score)
        {
            Word = word;
            Score = score;
        }

        public string Word { get;} 
        public int Score { get; }

    }

}
