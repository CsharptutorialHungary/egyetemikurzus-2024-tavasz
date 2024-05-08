using System.Text.Json;
using XWUH14.Domain.Entities;
using XWUH14.Domain.Interfaces;

namespace XWUH14.Infrastructure
{
    public class FileQuestionProvider : IQuestionProvider
    {
        private readonly string _filePath;

        public FileQuestionProvider(string filePath)
        {
            _filePath = filePath;
        }

        public async Task<IEnumerable<Question>> GetQuestionsAsync()
        {
            throw new Exception("Not implemented");
        }
    }
}
