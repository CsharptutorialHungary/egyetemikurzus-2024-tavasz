using System;

using BFR0QN.Etelek;

namespace BFR0QN;

class Program
{
    static void Main(string[] args)
    {
        
        GameManager g = new GameManager();
        Console.WriteLine(g.AtlagKcal());
        g.Betolt();
        
        int szint = 1;
        string beolvasSzoveg="";
        while (beolvasSzoveg != "k")
        {
            Hamburger burger = g.KovetkezoSzint(szint);
           String[] aktualisBurger = burger.Ingredients.ToArray();
            int i = 0;
            Console.Clear();
            Console.WriteLine("Anita - Szeretnék kérni egy " + burger.Name);
            Console.WriteLine("hozzávalok: ");
            g.help(aktualisBurger);
            while (i < aktualisBurger.Length)
            {
                Console.Write("{0}. elem:", i + 1);
                beolvasSzoveg = Console.ReadLine();
                if (beolvasSzoveg == ("?Burger"))
                {
                    g.help(aktualisBurger);
                }
                else if (beolvasSzoveg == aktualisBurger[i])
                {
                    i++;
                    Console.WriteLine("\n");
                }
                else if (beolvasSzoveg == "mentés")
                {
                    Console.Write("Mentés neve: ");
                    string mentesNeve = Console.ReadLine();
                    if (g.Mentes(mentesNeve,szint))
                    {
                        Console.WriteLine("Sikeres mentés");
                        g.JsonFileLetrehoz();
                    }
                    else
                    {
                        Console.WriteLine("Sikertelen mentés");
                    }
                }
                else
                {
                    Console.WriteLine("Elbasztad");
                }
            }
            Console.Clear();
            Console.WriteLine("Hurrá, Sikeresen teljesítetted a "+szint+". szintet!\n Jöhet a kövi?");
            Console.ReadKey();
            szint++;
        }
    }
}
