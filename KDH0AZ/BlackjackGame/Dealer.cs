using System.Collections.Generic;
using System;

public class Dealer
{
    public List<Card> Hand { get; }

    public Dealer()
    {
        Hand = new List<Card>();
    }

    public void ResetHand()
    {
        Hand.Clear();
    }
}
