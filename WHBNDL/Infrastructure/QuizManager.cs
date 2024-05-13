using System.Data.Entity;
using System.Data.SQLite;

using WHBNDL.Database;
using WHBNDL.Domain;

namespace WHBNDL.Infrastructure
{
    internal class QuizManager
    {
        private readonly Question[] _questions;
        private readonly char[] _answerOptions = ['A', 'B', 'C', 'D'];
        private int _correctAnswersCount = 0;
        private char _currentCorrectAnswer = 'A'; // Lényegtelen, csak hogy ne legyen hiba
        private bool _gameOver = false;
        private bool _restarted = false;
        private readonly MemoryDatabase _database;

        public QuizManager(Question[] questions, MemoryDatabase database)
        {
            _questions = questions;
            _database = database;
        }

        public void StartQuiz()
        {
            Console.WriteLine("Kezdődik a quiz! Nyomd meg az E gombot a kilépéshez, vagy az R gombot az újrakezdéshez.");
            QuizMain();
        }

        public void QuizMain()
        {
            foreach (var question in _questions)
            {
                if (_gameOver)
                    break;

                DisplayQuestion(question);
                EvaluateAnswer(question);
                Console.WriteLine();
            }
            if(!_restarted)
            {
                EndQuiz();
            }
            _restarted = false;
        }

        public void EndQuiz()
        {

            Console.WriteLine($"You got {_correctAnswersCount} out of {_questions.Length} questions correct.");
            _ = _database.SaveQuizResultAsync(_correctAnswersCount, _questions.Length);
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
                switch (userInput)
                {
                    case "E":
                        _gameOver = true;

                        break;
                    case "R":
                        _correctAnswersCount = 0;
                        _restarted = true;
                        QuizMain();
                        break;
                    default:
                        Console.WriteLine("Invalid input!\nGive a valid input!");
                        EvaluateAnswer(question);
                        break;
                }

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
