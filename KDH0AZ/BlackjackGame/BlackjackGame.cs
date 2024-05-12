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

}
