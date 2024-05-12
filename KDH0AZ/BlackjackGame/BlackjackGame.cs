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
        Console.WriteLine("\nAz Oszt� kez�ben l�v� els� lap:");
        Console.WriteLine($"{dealer.Hand[0].Rank} {dealer.Hand[0].Suit}");
    }

    public void PlaceBet() 
    {
        int betAmount = GetValidBetAmount(player.Money);

        if (betAmount > 0)
        {
            player.PlaceBet(betAmount);

            Console.WriteLine($"P�nzed: ${player.Money}, t�t: ${player.Bet}");

        }
        else
        {
            Console.WriteLine("Hib�s t�t�sszeg. A j�t�k nem indul el.");
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
            Console.Write("\nK�rlek, adj meg egy t�tet (minimum 1$): ");
            if (int.TryParse(Console.ReadLine(), out int betAmount) && betAmount >= 1 && betAmount <= availableMoney)
            {
                return betAmount;
            }

            Console.WriteLine("Hib�s t�t�sszeg. K�rlek, adj meg �rv�nyes t�tet.");
        }
    }

}
