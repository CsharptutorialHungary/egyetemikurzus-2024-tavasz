
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Kvizapp;

class Kviz {

    static public void Main(String[] args)
    {

        
        Player player = new Player();
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
            
            if (answer == "quit")
            {
                Console.WriteLine("You quit!");
                Console.WriteLine("Your points: " + player.points);
                break;
            }
            if (answer != questions[i].correct)
            {
                Console.WriteLine("Incorrect answer!");
                Console.WriteLine("Your points: " + player.points);
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
    
    bool checkAnswer(String answer, Question question)
    {
        return answer == question.correct;
    }

    public record Question
    {
        [JsonPropertyName("category")]
        public string category { get; set; }
        [JsonPropertyName("question")]
        public required string question { get; set; }
        [JsonPropertyName("a")]
        public required string aChoice { get; set; }
        [JsonPropertyName("b")]
        public required string bChoice { get; set; }
        [JsonPropertyName("c")]
        public required string cChoice { get; set; }
        [JsonPropertyName("d")]
        public required string dChoice { get; set; }
        [JsonPropertyName("correct")]
        public required string correct { get; set; }
        [JsonPropertyName("points")]
        public required int points { get; set; }
    }

    static public List<Question> loadQuestions()
    {
        string jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "questions.json");
        
        string json = File.ReadAllText(jsonFilePath);
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var triviaData = JsonSerializer.Deserialize<List<Question>>(json, options);

        return triviaData;
    }
}