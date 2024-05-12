class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Üdvözzöllek a BlackJack-en!\n");

        string playerName = GetValidPlayerName();

        if (playerName != null)
        {

            BlackjackGame game = new BlackjackGame(playerName);

            game.Start();

        }

    }

    static string GetValidPlayerName()
    {
        while (true)
        {
            try
            {
                Console.Write("Kérlek, add meg a neved: ");
                string? playerName = Console.ReadLine()?.Trim();

                if (!string.IsNullOrEmpty(playerName))
                {
                    return playerName;
                }

                throw new ArgumentException("Hibás bemenet. Kérlek, adj meg érvényes nevet.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }


}
