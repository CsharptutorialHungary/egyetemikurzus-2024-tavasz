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
            Console.WriteLine($"\n\n�dv�z�llek, {player.Name}! Egyenleged: ${player.Money}");
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
            Console.WriteLine("\nK�sz�nj�k a j�t�kot! Viszl�t!");
        }
    }

    private bool PlayAgain()
    {
        int playerMoney = player.Money;
        if (playerMoney > 0)
        {
            while (true)
            {
                Console.Write("\nSzeretn�l �j j�t�kot j�tszani? (igen/nem): ");
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
                    Console.WriteLine("Hib�s v�lasz. K�rem, adj meg �rv�nyes v�laszt (igen/nem).");
                }
            }
        }
        else 
        {
            Console.Write("\nSajn�lom, de elfogyott a p�nzed! ");
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
        Console.WriteLine("\nAz Oszt� kez�ben l�v� els� lap:");
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
                Console.WriteLine($"P�nzed: ${player.Money}, t�t: ${player.Bet}");
            }
            else
            {
                Console.WriteLine("Hib�s t�t�sszeg. A j�t�k nem indul el.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Hiba t�rt�nt a t�t megad�sakor: {ex.Message}");
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
            Console.Write("\nK�rsz m�g egy lapot? (igen/nem): ");
            string? answer = Console.ReadLine()?.ToLower();

            if (answer == "igen")
            {
                HitPlayer();
                DisplayingTabs();

                int playerHandValue = CalculateHandValue(player.Hand);

                if (playerHandValue > 21)
                {
                    Console.WriteLine("T�l sok lett a lapok �sszege (t�bb, mint 21)!");
                    continuePlaying = false;
                }
            }
            else if (answer == "nem")
            {
                Console.WriteLine("Meg�llt�l.");
                PlayDealerTurn();
                continuePlaying = false;
            }
            else
            {
                Console.WriteLine("Hib�s v�lasz. K�rem, adj meg �rv�nyes v�laszt (igen/nem).");
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

        Console.WriteLine("\nV�geredm�ny:");

        Console.WriteLine($"A j�t�kos lapjai:");
        foreach (var card in player.Hand)
        {
            Console.WriteLine($"{card.Rank} {card.Suit}");
        }
        Console.WriteLine($"J�t�kos lapjainak �ssz�rt�ke: {playerHandValue}");

        Console.WriteLine($"\nAz oszt� lapjai:");
        foreach (var card in dealer.Hand)
        {
            Console.WriteLine($"{card.Rank} {card.Suit}");
        }
        Console.WriteLine($"Oszt� lapjainak �ssz�rt�ke: {dealerHandValue}\n");

        if (playerHandValue > 21)
        {
            Console.WriteLine("J�t�kos vesztett! T�ll�pte a 21-et.");
        }
        else if (dealerHandValue > 21)
        {
            Console.WriteLine("J�t�kos nyert! Az oszt� t�ll�pte a 21-et.");
            player.AddMoney(player.Bet * 2);
        }
        else if (playerHandValue > dealerHandValue)
        {
            Console.WriteLine("J�t�kos nyert! Nagyobb �rt�k� lapokkal rendelkezik.");
            player.AddMoney(player.Bet * 2);
        }
        else if (playerHandValue < dealerHandValue)
        {
            Console.WriteLine("J�t�kos vesztett! Az oszt� lapjai �rt�kesebbek.");
        }
        else
        {
            Console.WriteLine("D�ntetlen! Egyenl� �rt�k� lapok.");
            player.AddMoney(player.Bet);
        }

        Console.WriteLine($"J�t�kos �j egyenlege: ${player.Money}");
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
            Console.Write("\nK�rlek, adj meg egy t�tet (minimum 1$): ");
            if (int.TryParse(Console.ReadLine(), out int betAmount) && betAmount >= 1 && betAmount <= availableMoney)
            {
                return betAmount;
            }

            Console.WriteLine("Hib�s t�t�sszeg. K�rlek, adj meg �rv�nyes t�tet.");
        }
    }

}
