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

        public void InGame(List<QuestionJson> questions)
        {
            foreach (QuestionJson question in questions)
            {
                host.WriteLine(question.Name);
                host.WriteLine(question.Answer_A);
                host.WriteLine(question.Answer_B);
                host.WriteLine(question.Answer_C);
                host.WriteLine(question.Answer_D);

                string answer = host.ReadLine();

                if(answer.ToLower().Equals(question.Correct))
                {
                    host.WriteLine("A válaszod helyes!");
                    user.Score++;
                }
                else
                {
                    host.WriteLine("A válaszod helytelen!");
                }
                Thread.Sleep(1500);
            }
            GameEnd();

        }

        public void GameEnd()
        {
            host.WriteLine("Köszönjük a játékot" +(user.UserName)+" !");
            host.WriteLine("Hamarosan visszaírányítunk a kezdőállapotra.!");
            Thread.Sleep(3000);

        }

        public List<QuestionJson> RandomQuestionsGenerator(QuestionDeserializer questionDeserializer)
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
