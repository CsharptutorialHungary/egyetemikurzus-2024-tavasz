using System;
using System.Security.Cryptography;

using BFR0QN.Etelek;

namespace BFR0QN;

class Program
{
    static async Task Main(string[] args)
    {

        GameManager g = GameManager.Instance;
        Console.WriteLine(g.AtlagKcal());
        await g.Betolt();
        
        int szint = g.getSzint();
        string beolvasSzoveg="";
        while (beolvasSzoveg != "k")
        {
            Etel etel = g.KovetkezoSzint(szint);
           String[] aktualisBurger = etel.Ingredients.ToArray();
            int i = 0;
            Console.Clear();
            Console.WriteLine("Anita - Szeretnék kérni egy " + etel.Name);
            Console.WriteLine("hozzávalok: ");
            g.help(aktualisBurger);
            while (i < aktualisBurger.Length)
            {
                Console.Write("{0}. elem:", i + 1);
                beolvasSzoveg = Console.ReadLine();
                if (beolvasSzoveg == ("?etel"))
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
