using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XWG8AW.Domain;
using XWG8AW.UserInterface;

namespace XWG8AW.Infrastructure
{
    internal class GameController
    {

        Host host = new Host();
        User user = new User();

        public void ReadUserName()
        {
            host.WriteLine("Adja meg a játékos nevét!");
            user.UserName = host.ReadLine();
            user.Score = 0;

            while(user.UserName == null)
            {
                host.WriteLine("Érvénytelen név adjon meg egy másikat");
                user.UserName = host.ReadLine();
            }
        }

        public void Gamestart()
        {
            host.WriteLine("A játék hamarosan indul 10 feladatot kapsz sok sikert!");
            host.Write("A játék hamarosan kezdődik.");

            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(900);
                host.Write(".");
            }

            Console.Clear();
        }

        public void InGame()
        {

        }

        public List<QuestionJson> RandomQuestion(QuestionDeserializer questionDeserializer)
        {
            Task<List<QuestionJson>> questionList =  questionDeserializer.QuestionDeserializeFromJson();

            List<QuestionJson> randomQuestions = new List<QuestionJson>();

            int questionCount = 10;

            for (int i = 0; i < questionCount; i++)
            {
                int randomNumber = new Random().Next(1, questionList.Result.Count);

                randomQuestions.Add(questionList.Result[randomNumber]);
            }

            return randomQuestions;
        }
    }
}
