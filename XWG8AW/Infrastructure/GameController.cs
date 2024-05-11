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
        public User User = new User();

        public void ReadUserName()
        {
            host.WriteLine("Adja meg a játékos nevét!");
            User.UserName = host.ReadLine();
            User.Score = 0;

            while(User.UserName == null)
            {
                host.WriteLine("Érvénytelen név adjon meg egy másikat");
                User.UserName = host.ReadLine();
            }
        }

        public void Gamestart()
        {
            host.WriteLine("A játék hamarosan indul 10 feladatot kapsz sok sikert!");
            host.WriteLine("A kérdésre csak az 'A', 'B', 'C' vagy 'D' betűk megadásával válaszolj!");
            host.WriteLine("Más válasz megadása helytelen megoldást eredményez!");
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
                    User.Score++;
                }
                else
                {
                    host.WriteLine("A válaszod helytelen!");
                }
                Thread.Sleep(1500);
                Console.Clear();
            }
            GameEnd();

        }

        public void GameEnd()
        {
            host.WriteLine("Köszönjük a játékot "+(User.UserName)+"!");
            host.WriteLine("A pontszámod: "+(User.Score));
            host.WriteLine("Hamarosan visszaírányítunk a kezdőállapotra.");
            Thread.Sleep(3000);
            Console.Clear();

        }

        public List<QuestionJson> RandomQuestionsGenerator(QuestionDeserializer questionDeserializer)
        {
            Task<List<QuestionJson>> questionList =  questionDeserializer.QuestionDeserializeFromJson();

            List<QuestionJson> randomQuestions = new List<QuestionJson>();

            int questionCount = 3;

            for (int i = 0; i < questionCount; i++)
            {
                int randomNumber = new Random().Next(0, questionList.Result.Count);

                if(i == 0)
                {
                    randomQuestions.Add(questionList.Result[randomNumber]);
                    continue;
                }

                for (int j = 0; j < randomQuestions.Count; j++)
                {
                    if (!(randomQuestions[j].Name.Equals(questionList.Result[randomNumber].Name)))
                    {
                        randomQuestions.Add(questionList.Result[randomNumber]);
                        break;
                    }
                    else
                    {
                        i--;
                    }
                }
            }

            return randomQuestions;
        }
    }
}
