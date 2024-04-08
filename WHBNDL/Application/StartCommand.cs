using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WHBNDL.Domain;
using WHBNDL.Infrastructure;

namespace WHBNDL.Application
{
    internal class StartCommand : IShellCommand
    {
        public string Name => "start";

        public void Execute(IHost host, string[] args)
        {
            Question[] questions = QuestionsDeserializer.Deserialize();
            var quizManager = new QuizManager(questions);
            quizManager.StartQuiz();
        }

    }
}
