using System.Collections.Generic;

public class Deck
{
    private List<Card> cards;

    public Deck()
    {
        cards = new List<Card>();
        string[] suits = { "Hearts", "Diamonds", "Clubs", "Spades" };
        string[] ranks = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace" };

        foreach (var suit in suits)
        {
            foreach (var rank in ranks)
            {
                cards.Add(new Card(suit, rank));
            }
        }
    }

    public List<Card> GetShuffledDeck()
    {
        return cards.OrderBy(c => Guid.NewGuid()).ToList();
    }
}
