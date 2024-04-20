using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBNMY
{
    internal sealed record PlayerScore
    {
        public string Word { get; init; }
        public int Score { get; init; }

        public PlayerScore(string word, int score)
        {
            Word = word;
            Score = score;
        }
    }

}
