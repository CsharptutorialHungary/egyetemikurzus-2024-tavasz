using System;


namespace CVBNMY
{
    class Program
    {
        public static void Main(string[] args)
        {
            HangmanGame hangmanGame = new HangmanGame(Difficulty.EASY);
            Console.WriteLine(hangmanGame.ToString());
        }
    }
}
