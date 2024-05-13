using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHBNDL.Domain
{
    internal class QuizResult
    {
        public int CorrectAnswers { get; set; }
        public int TotalQuestions { get; set; }
        public DateTime Timestamp { get; set; }
        public string Message { get; set; }

        public QuizResult(int correctAnswers, int totalQuestions, DateTime timestamp)
        {
            CorrectAnswers = correctAnswers;
            TotalQuestions = totalQuestions;
            Timestamp = timestamp;
        }

        public QuizResult(string message)
        {
            Message = message;
        }
    }
}
