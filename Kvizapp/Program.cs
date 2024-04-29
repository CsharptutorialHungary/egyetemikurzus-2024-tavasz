
using System;

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

        List<Question> questions = generateTriviaQuestions();
        
        
    }

    public record Player
    {
        public String name = "";
        public int points = 0;
    };

    public record Question(
        String question,
        String aChoice,
        String bChoice,
        String cChoice,
        String dChoice,
        String answer,
        int points
    );


    static List<Question> generateTriviaQuestions()
    {
        List<Question> questions = new List<Question>
        {
            new Question("What is the capital of France?", "Paris", "Madrid", "Moscow", "Kiev", "Paris" , 10),
            new Question("Who has scored against the most teams?", "Luis Suarez", "Zlatan Ibrahimovic", "Cristiano Ronaldo", "Lionel Messi", "Zlatan Ibrahimovic", 10),
            new Question("Who earned the most olympic gold medals?", "Michael Phelps", "Carl Lewis", "Mark Spitz", "Larisa Latynina", "Michael Phelps", 10),
            new Question("What currency did Austria use before the euro?", "Pound", "Dollar", "Schilling", "Mark", "Schilling", 10),
            new Question("What is Germany's domain name?", ".ge", ".ne", ".de", ".gr", ".de", 10),
            new Question("What sides fought during the battle of Marathon?", "romans and persians", "greeks and persians", "greeks and romans", "romans and puns", "greeks and persians", 10),
            new Question("What color is sulfur?", "Blue", "Yellow", "Red", "White", "Yellow", 10),
            new Question("How wide is a football goal?", "6,5 meters", "3 meters", "8 meters", "7,32 meters", "7,32 meters", 10),
            new Question("What color is NOT in the olympic flag?", "Blue", "Black", "Orange", "Red", "Orange", 10),
            new Question("What country is ice cream originally from?", "Russia", "USA", "Germany", "China", "China", 10)
        };
        return questions;
    }
}