using System.Collections.Generic;
using System;

public class BlackjackGame
{
    private Player player;
    private List<Card> deck;

    public BlackjackGame(string playerName)
    {
        player = new Player(playerName);
        deck = new Deck().GetShuffledDeck();
    }

    public void Start()
    {
        Console.WriteLine($"\n\nUdvozollek, {player.Name}! Kezdo penzosszeged: ${player.Money}");
        TetFeladasa();
        DealInitialCards();
    }

    public void TetFeladasa() 
    {
        int betAmount = GetValidBetAmount(player.Money);

        if (betAmount > 0)
        {
            player.PlaceBet(betAmount);

            Console.WriteLine($"Pénzed: ${player.Money}, tét: ${player.Bet}");

        }
        else
        {
            Console.WriteLine("Hibás tétösszeg. A játék nem indul el.");
        }

    }

    public void DealInitialCards()
    {
        player.Hand.Add(deck.First());
        deck.RemoveAt(0);
        player.Hand.Add(deck.First());
        deck.RemoveAt(0);

        Console.WriteLine("A kezedben van:");
        foreach (var card in player.Hand)
        {
            Console.WriteLine($"{card.Rank} {card.Suit}");
        }
        Console.WriteLine("Mit fogsz tenni?");
    }


    static int GetValidBetAmount(int availableMoney)
    {
        while (true)
        {
            Console.Write("Kérlek, adj meg egy tétet (minimum 1$): ");
            if (int.TryParse(Console.ReadLine(), out int betAmount) && betAmount >= 1 && betAmount <= availableMoney)
            {
                return betAmount;
            }

            Console.WriteLine("Hibás tétösszeg. Kérlek, adj meg érvényes tétet.");
        }
    }

}
