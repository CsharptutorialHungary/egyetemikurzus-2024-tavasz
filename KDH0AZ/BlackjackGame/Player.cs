using System.Collections.Generic;

public class Player
{
    public string Name { get; }
    public List<Card> Hand { get; }

    public Player(string name)
    {
        Name = name;
        Hand = new List<Card>();
    }
}
