using XWUH14.Domain.Entities;
using XWUH14.Domain.Interfaces;

namespace XWUH14.Infrastructure
{
    public class AnswerValidator : IAnswerValidator
    {
        public bool ValidateAnswer(Question question, Answer answer)
        {
            return string.Equals(question.CorrectAnswer, answer.Text, StringComparison.OrdinalIgnoreCase);
        }
    }
}
