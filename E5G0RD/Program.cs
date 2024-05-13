using E5G0RD.Infrastructure;
using E5G0RD.UserInterface;

public class Program
{
    static async Task Main(string[] args)
    {
        var wordRepository = new WordRepository();
        var words = await wordRepository.LoadWordsAsync("words.json");

        foreach (var word in words)
        {
            Console.WriteLine(word.Value);
        }

        var game = new Game(words);
        game.Start();
    }
}