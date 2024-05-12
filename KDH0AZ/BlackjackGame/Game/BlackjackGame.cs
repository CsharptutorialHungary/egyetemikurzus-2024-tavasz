using System.Collections.Generic;
using System;
using System.Linq;
using System.Text.Json;

using Blackjack.Models;

namespace Blackjack.Game
{
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
                Console.WriteLine($"Üdvözöllek, {player.Name}! Egyenleged: ${player.Money}");
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
                if (player.Money > 0)
                {
                    DataUpload();
                }
            }
        }

        public void DataUpload()
        {
            var playerData = new
            {
                Name = player.Name,
                Money = player.Money
            };

            string filePath = "F:\\Alkfejlesztes charp\\KDH0AZ\\BlackjackGame\\data.json";

            try
            {
                bool fileExists = File.Exists(filePath);

                string jsonData = string.Empty;

                if (fileExists)
                {
                    jsonData = File.ReadAllText(filePath);
                }

                List<object> dataList = new List<object>();

                if (!string.IsNullOrEmpty(jsonData))
                {
                    dataList = JsonSerializer.Deserialize<List<object>>(jsonData);
                }

                dataList.Add(playerData);

                string finalJson = JsonSerializer.Serialize(dataList, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                File.WriteAllText(filePath, finalJson);

                Console.Clear();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba történt a fájl mentésekor: {ex.Message}");
            }
        }

        public void PlaceBet()
        {
            try
            {
                int betAmount = GetValidBetAmount(player.Money);

                if (betAmount > 0)
                {
                    Console.Clear();
                    player.PlaceBet(betAmount);
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

        public void DisplayingTabs()
        {
            Console.WriteLine($"Pénzed: ${player.Money}, tét: ${player.Bet}\n");
            Console.WriteLine("A kezedben van:");
            foreach (var card in player.Hand)
            {
                Console.WriteLine($"{card.Rank} {card.Suit}");
            }
            Console.WriteLine("\nAz Osztó kezében lévõ elsõ lap:");
            Console.WriteLine($"{dealer.Hand[0].Rank} {dealer.Hand[0].Suit}");
        }

        public void PlayPlayerTurn()
        {
            bool continuePlaying = true;
            while (continuePlaying)
            {
                Console.Write("\nKérsz még egy lapot? (igen/nem): ");
                string? answer = Console.ReadLine()?.ToLower();

                if (answer == "igen" || answer == "i" || answer == "yes" || answer == "y")
                {
                    Console.Clear();
                    HitPlayer();
                    DisplayingTabs();

                    int playerHandValue = CalculateHandValue(player.Hand);

                    if (playerHandValue > 21)
                    {
                        continuePlaying = false;
                        Console.Clear();
                    }
                }
                else if (answer == "nem" || answer == "n" || answer == "no")
                {
                    PlayDealerTurn();
                    continuePlaying = false;
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("Hibás válasz. Kérem, adj meg érvényes választ (igen/nem).");
                }
            }

        }

        private void EndGame()
        {
            int playerHandValue = CalculateHandValue(player.Hand);
            int dealerHandValue = CalculateHandValue(dealer.Hand);

            Console.WriteLine("Végeredmény:");

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


        private bool PlayAgain()
        {
            int playerMoney = player.Money;
            if (playerMoney > 0)
            {
                while (true)
                {
                    Console.Write("\nSzeretnél új játékot játszani? (igen/nem): ");
                    string answer = Console.ReadLine()?.ToLower() ?? "";

                    if (answer == "igen" || answer == "i" || answer == "yes" || answer == "y")
                    {
                        Console.Clear();
                        return true;
                    }
                    else if (answer == "nem" || answer == "n" || answer == "no")
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
                Console.ReadKey(true);
                Console.Clear();
                return false;
            }

        }

        private void ResetGame()
        {
            player.ResetHand();
            dealer.ResetHand();
            deck = new Deck().GetShuffledDeck();
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
                dealer.Hand.Add(deck.First());
                deck.RemoveAt(0);

                dealerHandValue = CalculateHandValue(dealer.Hand);

                if (dealerHandValue > 21 || dealerHandValue > playerHandValue)
                {
                    break;
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

                if (card.Rank == "Ász")
                {
                    aceCount++;
                }
            }

            for (int i = 0; i < aceCount; i++)
            {
                if (value > 21 && aceCount > 1 && i == aceCount - 1)
                {
                    value -= 10;
                }
                else if (value > 21 && aceCount == 1)
                {
                    value -= 10;
                }
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
}