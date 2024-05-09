using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XWUH14.Domain.Entities
{
    public class Question
    {
        public string Text { get; }
        public string CorrectAnswer { get; }

        public Question(string text, string correctAnswer)
        {
            Text = text;
            CorrectAnswer = correctAnswer;
        }
    }
}
