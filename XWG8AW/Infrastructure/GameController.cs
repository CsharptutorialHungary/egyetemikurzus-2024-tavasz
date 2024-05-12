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
            host.WriteLine("Adja meg a jatekos nevet!");
            User.UserName = host.ReadLine();
            User.Score = 0;

            while(User.UserName == "" || User.UserName == null)
            {
                host.WriteLine("Ervenytelen nev adjon meg egy masikat");
                User.UserName = host.ReadLine();
            }
        }

        public void Gamestart()
        {
            host.WriteLine("A jaték hamarosan indul 10 kerdest kapsz sok sikert!");
            host.WriteLine("A kerdesre csak az 'A', 'B', 'C' vagy 'D' betuk megadasaval valaszolj!");
            host.WriteLine("Mas valasz megadasa helytelen megoldast eredmenyez!");
            host.Write("A jatek hamarosan kezdodik.");

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
                host.WriteLine("Kerdes: "+question.Name);
                host.WriteLine("A: "+question.Answer_A);
                host.WriteLine("B: "+question.Answer_B);
                host.WriteLine("C: "+question.Answer_C);
                host.WriteLine("D: "+question.Answer_D);

                string answer = host.ReadLine();

                if(answer.ToLower().Equals(question.Correct))
                {
                    host.WriteLine("A valaszod helyes!");
                    User.Score++;
                }
                else
                {
                    host.WriteLine("A valaszod helytelen!");
                }
                Thread.Sleep(1500);
                Console.Clear();
            }
            GameEnd();

        }

        public void GameEnd()
        {
            host.WriteLine("Koszonjuk a jatekot "+(User.UserName)+"!");
            host.WriteLine("A pontszamod: "+(User.Score));
            host.WriteLine("Hamarosan visszairanyitunk a kezdoallapotra.");
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
                        break;
                    }
                }
            }

            return randomQuestions;
        }
    }
}
