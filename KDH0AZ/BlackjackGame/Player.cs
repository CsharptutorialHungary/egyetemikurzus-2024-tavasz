using System;
using System.Collections.Generic;

public class Player
{
    public string Name { get; }
    public List<Card> Hand { get; }
    public int Money { get; private set; }
    public int Bet { get; private set; }

    public Player(string name, int startingMoney = 100)
    {
        Name = name;
        Hand = new List<Card>();
        Money = startingMoney;
        Bet = 0;
    }

    public void ModifyMoney(int amount)
    { 
        Money += amount;
    }

    public void PlaceBet(int amount)
    {
        if (amount > Money)
        {
            Console.WriteLine("Nincs elegendõ pénz a tét feladásához!");
        }
        else
        {
            Bet = amount;
            Money -= amount;
            Console.WriteLine($"{Name} feladta a ${Bet} tétet.");
        }
    }

}
