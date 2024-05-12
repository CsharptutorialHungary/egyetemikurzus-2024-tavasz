using System.Collections.Generic;
using System;
using System.Linq;

public class BlackjackGame
{
    private Player player;
    private Dealer dealer;
    private List<Card> deck;
    private bool firstGame = true;

    public BlackjackGame(string playerName)
    {
        player = new Player(playerName);
        dealer = new Dealer();
        deck = new Deck().GetShuffledDeck();
    }

    public void Start()
    {
        if (firstGame)
        {
            Console.WriteLine($"\n\nÜdvözöllek, {player.Name}! Egyenleged: ${player.Money}");
            firstGame = false;
        }
        else
        {
            Console.WriteLine($"Egyenleged: ${player.Money}");
        }
     
        PlayRound();
    }

    private void PlayRound()
    {
        PlaceBet();
        PlayerDealInitialCards();
        DealerDealInitialCards();
        DisplayingTabs();
        PlayPlayerTurn();
        EndGame();

        if (PlayAgain())
        {
            ResetGame();
            Start();
        }
        else
        {
            Console.WriteLine("\nKöszönjük a játékot! Viszlát!");
        }
    }

    private bool PlayAgain()
    {
        int playerMoney = player.Money;
        if (playerMoney > 0)
        {
            while (true)
            {
                Console.Write("\nSzeretnél új játékot játszani? (igen/nem): ");
                string answer = Console.ReadLine()?.ToLower() ?? "";

                if (answer == "igen")
                {
                    return true;
                }
                else if (answer == "nem")
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Hibás válasz. Kérem, adj meg érvényes választ (igen/nem).");
                }
            }
        }
        else 
        {
            Console.Write("\nSajnálom, de elfogyott a pénzed! ");
            return false;
        }
        
    }

    private void ResetGame()
    {
        player.ResetHand();
        dealer.ResetHand();
        deck = new Deck().GetShuffledDeck();
    }


    public void DisplayingTabs()
    {
        Console.WriteLine("\nA kezedben van:");
        foreach (var card in player.Hand)
        {
            Console.WriteLine($"{card.Rank} {card.Suit}");
        }
        Console.WriteLine("\nAz Osztó kezében lévõ elsõ lap:");
        Console.WriteLine($"{dealer.Hand[0].Rank} {dealer.Hand[0].Suit}");
    }

    public void PlaceBet()
    {
        try
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
        catch (Exception ex)
        {
            Console.WriteLine($"Hiba történt a tét megadásakor: {ex.Message}");
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
            string? answer = Console.ReadLine()?.ToLower();

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
                PlayDealerTurn();
                continuePlaying = false;
            }
            else
            {
                Console.WriteLine("Hibás válasz. Kérem, adj meg érvényes választ (igen/nem).");
            }
        }
    }

    private void PlayDealerTurn()
    {
        foreach (var card in dealer.Hand)
        {
            Console.WriteLine($"{card.Rank} {card.Suit}");
        }

        int dealerHandValue = CalculateHandValue(dealer.Hand);
        int playerHandValue = CalculateHandValue(player.Hand);

        while (dealerHandValue < 17)
        {
            Console.WriteLine(dealerHandValue);
            dealer.Hand.Add(deck.First());
            deck.RemoveAt(0);

            dealerHandValue = CalculateHandValue(dealer.Hand);

            if (dealerHandValue > 21)
            {
                break;
            }
        }
    }


    private void EndGame()
    {
        int playerHandValue = CalculateHandValue(player.Hand);
        int dealerHandValue = CalculateHandValue(dealer.Hand);

        Console.WriteLine("\nVégeredmény:");

        Console.WriteLine($"A játékos lapjai:");
        foreach (var card in player.Hand)
        {
            Console.WriteLine($"{card.Rank} {card.Suit}");
        }
        Console.WriteLine($"Játékos lapjainak összértéke: {playerHandValue}");

        Console.WriteLine($"\nAz osztó lapjai:");
        foreach (var card in dealer.Hand)
        {
            Console.WriteLine($"{card.Rank} {card.Suit}");
        }
        Console.WriteLine($"Osztó lapjainak összértéke: {dealerHandValue}\n");

        if (playerHandValue > 21)
        {
            Console.WriteLine("Játékos vesztett! Túllépte a 21-et.");
        }
        else if (dealerHandValue > 21)
        {
            Console.WriteLine("Játékos nyert! Az osztó túllépte a 21-et.");
            player.AddMoney(player.Bet * 2);
        }
        else if (playerHandValue > dealerHandValue)
        {
            Console.WriteLine("Játékos nyert! Nagyobb értékû lapokkal rendelkezik.");
            player.AddMoney(player.Bet * 2);
        }
        else if (playerHandValue < dealerHandValue)
        {
            Console.WriteLine("Játékos vesztett! Az osztó lapjai értékesebbek.");
        }
        else
        {
            Console.WriteLine("Döntetlen! Egyenlõ értékû lapok.");
            player.AddMoney(player.Bet);
        }

        Console.WriteLine($"Játékos új egyenlege: ${player.Money}");
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
