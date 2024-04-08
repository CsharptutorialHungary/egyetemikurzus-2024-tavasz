using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WHBNDL.Domain;

namespace WHBNDL.Infrastructure
{
    internal class QuizManager
    {
        private readonly Question[] _questions;
        private readonly char[] _answerOptions = ['A', 'B', 'C', 'D'];
        private int _correctAnswersCount = 0;
        private  char _currentCorrectAnswer = 'A'; // Lényegtelen, csak hogy ne legyen hiba

        public QuizManager(Question[] questions)
        {
            _questions = questions;
        }

        public void StartQuiz()
        {
            foreach (var question in _questions)
            {
                DisplayQuestion(question);
                EvaluateAnswer(question);
                Console.WriteLine();
            }

            Console.WriteLine($"You got {_correctAnswersCount} out of {_questions.Length} questions correct.");
        }

        private void DisplayQuestion(Question question)
        {
            Console.WriteLine(question.QuestionText);


            List<string> allAnswers = new List<string>();
            allAnswers.Add(question.Answers.CorrectAnswer);
            allAnswers.AddRange(question.Answers.WrongAnswers);

            Random rng = new Random();
            allAnswers = allAnswers.OrderBy(a => rng.Next()).ToList();

            for (int i = 0; i < allAnswers.Count; i++)
            {
                Console.WriteLine($"{_answerOptions[i]}) {allAnswers[i]}");
                if (allAnswers[i] == question.Answers.CorrectAnswer)
                {
                    _currentCorrectAnswer = _answerOptions[i];
                }
            }
        }

        private void EvaluateAnswer(Question question)
        {
            Console.Write("Your answer (A/B/C/D): ");
            string userInput = Console.ReadLine()?.Trim().ToUpper();

            if (string.IsNullOrEmpty(userInput) || userInput.Length != 1 || !_answerOptions.Contains(userInput[0]))
            {
                Console.WriteLine("Invalid input!\nGive a valid input!");
                EvaluateAnswer(question);
            }
            else
            {
                
                if (userInput[0] == _currentCorrectAnswer)
                {
                    Console.WriteLine("Correct!");
                    _correctAnswersCount++;
                }
                else
                {
                    Console.WriteLine("Incorrect!");
                }
            }
        }
    }
}
