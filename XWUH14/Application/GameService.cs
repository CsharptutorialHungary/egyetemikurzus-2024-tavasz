﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XWUH14.Domain.Entities;
using XWUH14.Domain.Interfaces;

namespace XWUH14.Application
{
    public class GameService : IGameService
    {
        private readonly IQuestionProvider _questionProvider;
        private readonly IAnswerValidator _answerValidator;
        private readonly PlayerService _playerService;

        public GameService(IQuestionProvider questionProvider, IAnswerValidator answerValidator, PlayerService playerService)
        {
            _questionProvider = questionProvider;
            _answerValidator = answerValidator;
            _playerService = playerService;
        }

        public async Task StartGameAsync()
        {
            var questions = await _questionProvider.GetQuestionsAsync();

            if (!questions.Any())
            {
                Console.WriteLine("Nincsenek elérhető kérdések, így nem tudjuk a játékot elindítani.");
                return;
            }

            foreach (var question in questions)
            {
                Console.WriteLine(question.Text);
                Console.WriteLine($"Válasz: {question.CorrectAnswer}");
            }   

        }
    }
}