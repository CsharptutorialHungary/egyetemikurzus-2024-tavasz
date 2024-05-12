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
        PlayPlayerTurn();
    }

    public void DisplayingTabs()
    {
        Console.WriteLine("\nA kezedben van:");
        foreach (var card in player.Hand)
        {
            Console.WriteLine($"{card.Rank} {card.Suit} {card.Value}");
        }
        Console.WriteLine("\nAz Osztó kezében lévõ elsõ lap:");
        Console.WriteLine($"{dealer.Hand[0].Rank} {dealer.Hand[0].Suit} {dealer.Hand[0].Value}");
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

    public void PlayPlayerTurn() 
    {
        bool continuePlaying = true;
        while (continuePlaying)
        {
            Console.Write("\nKérsz még egy lapot? (igen/nem): ");
            string answer = Console.ReadLine()?.ToLower();

            if (answer == "igen")
            {
                HitPlayer();
                DisplayingTabs();

                int playerHandValue = CalculateHandValue(player.Hand);
                if (playerHandValue > 21)
                {
                    Console.WriteLine("Túl sok lett a lapok összege (több, mint 21)!");
                    continuePlaying = false;
                }
            }
            else if (answer == "nem")
            {
                Console.WriteLine("Megálltál.");

                continuePlaying = false;
            }
            else
            {
                Console.WriteLine("Hibás válasz. Kérem, adj meg érvényes választ (igen/nem).");
            }
        }
    }

    private void HitPlayer()
    {
        player.Hand.Add(deck.First());
        deck.RemoveAt(0);
    }

    private int CalculateHandValue(List<Card> hand)
    {
        int value = 0;
        int aceCount = 0;

        foreach (var card in hand)
        {
            value += card.Value;

            if (card.Rank == "Ace")
            {
                aceCount++;
            }
        }

        while (aceCount > 0 && value > 21)
        {
            value -= 10;
            aceCount--;
        }

        return value;
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
