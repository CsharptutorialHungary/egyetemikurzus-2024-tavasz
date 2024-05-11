using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XWG8AW.Infrastructure;

namespace XWG8AW.Application
{
    internal class StartCommand : IShellCommand
    {
        public string Name => "start";

        public void Execute(IHost host, string[] args)
        {
            GameController gameController = new GameController();

            QuestionDeserializer questionDeserializer = new QuestionDeserializer();

            gameController.ReadUserName();
            
            gameController.Gamestart();
            gameController.InGame(gameController.RandomQuestionsGenerator(questionDeserializer));
        }
    }
}
