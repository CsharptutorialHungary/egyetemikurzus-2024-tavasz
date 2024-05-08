using XWUH14.Domain.Entities;

namespace XWUH14.Domain.Interfaces
{
    public interface IQuestionProvider
    {
        Task<IEnumerable<Question>> GetQuestionsAsync();
    }
}
