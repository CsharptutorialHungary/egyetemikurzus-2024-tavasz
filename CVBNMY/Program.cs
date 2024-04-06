using System;


namespace CVBNMY
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            HangmanGame hangmanGame = new HangmanGame(Difficulty.HARD);
            Console.WriteLine(hangmanGame.ToString());
            Console.WriteLine();
            await hangmanGame.HangmanGameTask();
        }
    }
}
