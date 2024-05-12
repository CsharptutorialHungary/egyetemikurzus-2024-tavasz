using System.Collections.Generic;
using System;
using System.Linq;

public class BlackjackGame
{
    private Player player;
    private Dealer dealer;
    private List<Card> deck;

    public BlackjackGame(string playerName)
    {
        player = new Player(playerName);
        dealer = new Dealer();
        deck = new Deck().GetShuffledDeck();
    }

    public void Start()
    {
        Console.WriteLine($"\n\nUdvozollek, {player.Name}! Kezdo penzosszeged: ${player.Money}");
        PlaceBet();
        PlayerDealInitialCards();
        DealerDealInitialCards();
        DisplayingTabs();
    }

    public void DisplayingTabs()
    {
        Console.WriteLine("A kezedben van:");
        foreach (var card in player.Hand)
        {
            Console.WriteLine($"{card.Rank} {card.Suit}");
        }
        Console.WriteLine("\nAz Osztó kezében lévõ elsõ lap:");
        Console.WriteLine($"{dealer.Hand[0].Rank} {dealer.Hand[0].Suit}");
    }

    public void PlaceBet() 
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

    public void PlayerDealInitialCards()
    {
        player.Hand.Add(deck.First());
        deck.RemoveAt(0);
        player.Hand.Add(deck.First());
        deck.RemoveAt(0);
    }

    public void DealerDealInitialCards()
    {
        dealer.Hand.Add(deck.First());
        deck.RemoveAt(0);
        dealer.Hand.Add(deck.First());
        deck.RemoveAt(0);
    }

    static int GetValidBetAmount(int availableMoney)
    {
        while (true)
        {
            Console.Write("\nKérlek, adj meg egy tétet (minimum 1$): ");
            if (int.TryParse(Console.ReadLine(), out int betAmount) && betAmount >= 1 && betAmount <= availableMoney)
            {
                return betAmount;
            }

            Console.WriteLine("Hibás tétösszeg. Kérlek, adj meg érvényes tétet.");
        }
    }

}
