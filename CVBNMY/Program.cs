using System;


namespace CVBNMY
{
    class Program
    {
        public static async Task Main(string[] args)
        {
        
            HangmanGame hg = new HangmanGame();
            await hg.HangmanGameTask();

        }
    }
}

