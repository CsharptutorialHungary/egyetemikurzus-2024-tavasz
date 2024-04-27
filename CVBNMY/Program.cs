using System;

using CVBNMY.Application;


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

