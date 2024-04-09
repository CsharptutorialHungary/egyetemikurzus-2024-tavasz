using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHBNDL.Domain
{
    internal class QuizResult
    {
        public int CorrectAnswers { get; }
        public int TotalQuestions { get; }
        public DateTime Timestamp { get; }

        public QuizResult(int correctAnswers, int totalQuestions, DateTime timestamp)
        {
            CorrectAnswers = correctAnswers;
            TotalQuestions = totalQuestions;
            Timestamp = timestamp;
        }
    }
}
