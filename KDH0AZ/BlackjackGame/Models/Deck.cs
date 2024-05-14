using System.Collections.Generic;

namespace Blackjack.Models
{
    public class Deck
    {
        private List<Card> cards;

        public Deck()
        {
            cards = new List<Card>();
            InitializeDeck();
        }

        private void InitializeDeck()
        {
            string[] suits = { "Kör", "Káró", "Treff", "Pikk" };
            Dictionary<string, int> rankValues = new Dictionary<string, int>
        {
            { "2", 2 },
            { "3", 3 },
            { "4", 4 },
            { "5", 5 },
            { "6", 6 },
            { "7", 7 },
            { "8", 8 },
            { "9", 9 },
            { "10", 10 },
            { "Bubi", 10 },
            { "Dáma", 10 },
            { "Király", 10 },
            { "Ász", 11 }
        };

            foreach (var suit in suits)
            {
                foreach (var rank in rankValues.Keys)
                {
                    int value = rankValues[rank];
                    cards.Add(new Card(suit, rank, value));
                }
            }
        }


        public List<Card> GetShuffledDeck()
        {
            return cards.OrderBy(c => Guid.NewGuid()).ToList();
        }
    }
}