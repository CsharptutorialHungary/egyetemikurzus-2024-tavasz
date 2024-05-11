
using System;
using System.Text.Json;

namespace Kvizapp;

class Kviz {

    static public void Main(String[] args)
    {

        
        Player player = new Player();
        Console.WriteLine(Directory.GetCurrentDirectory());
        Console.WriteLine("Please add your player's name!");
        
        try
        {
            player.name = Console.ReadLine();

            if (player.name.Length == 0)
            {
                throw new ArgumentException("No valid name given!");
            }
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
        }
        List<Question> questions = loadQuestions();
        
        for (int i = 0; i < questions.Count; i++)
        {
            Console.WriteLine(questions[i].question);
            Console.WriteLine("A. " + questions[i].aChoice);
            Console.WriteLine("B. " + questions[i].bChoice);
            Console.WriteLine("C. " + questions[i].cChoice);
            Console.WriteLine("D. " + questions[i].dChoice);

            String answer = Console.ReadLine();

            if (answer != questions[i].answer)
            {
                Console.WriteLine("Incorrect answer!");
                break;
            }
            else
            {
                player.points = player.points + questions[i].points;
                Console.WriteLine("Good job! +10 points");
                Console.WriteLine("Your current point: " + player.points);
            }
        }
    }

    public record Player
    {
        public String name = "";
        public int points = 0;
    };

    public record Question(
        String category,
        String question,
        String aChoice,
        String bChoice,
        String cChoice,
        String dChoice,
        String answer,
        int points
    );

    bool checkAnswer(String answer, Question question)
    {
        return answer == question.answer;
    }


    static public List<Question> loadQuestions()
    {
        string jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "questions.json");
        
        string json = File.ReadAllText(jsonFilePath);
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var triviaData = JsonSerializer.Deserialize<Question>(json, options);

        return null;
    }
}