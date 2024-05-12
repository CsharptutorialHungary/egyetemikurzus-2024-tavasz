class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Üdvözzöllek a BlackJack-en!\n");

        string playerName = GetValidPlayerName();

        if (playerName != null)
        {
            BlackjackGame game = new BlackjackGame(playerName);

            game.DealInitialCards();

        }

    }

    static string GetValidPlayerName()
    {
        while (true)
        {
            Console.Write("Kérlek, add meg a neved: ");
            string? playerName = Console.ReadLine()?.Trim();

            if (!string.IsNullOrEmpty(playerName))
            {
                return playerName;
            }

            Console.WriteLine("Hibas bemenet. Kerlek, adj meg ervenyes nevet.");
        }
    }

}
