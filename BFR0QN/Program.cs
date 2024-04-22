using BFR0QN.Burger;

namespace BFR0QN;

class Program
{
    static void Main(string[] args)
    {
        GameManager g = new GameManager();
        g.Betolt();
        int szint = 0;
        Hamburger burger= g.kovetkezoSzint(szint);
        String[] aktualisBurger = burger.tomb();
        Console.WriteLine("Anita - Szeretnék kérni egy "+burger.nev());
        string beolvasSzoveg;
        int i = 0;
        while (i<aktualisBurger.Length) {
            Console.Write("{0}. elem:", i + 1);
            beolvasSzoveg= Console.ReadLine();
            if (beolvasSzoveg == ("?pistiBurger"))
            {
                for (int k = 0; k < aktualisBurger.Length; k++)
                {
                    int utolsoElemE = k + 1;
                    if (utolsoElemE == aktualisBurger.Length)
                    {
                        Console.Write(aktualisBurger.Length);
                    }
                    Console.Write(aktualisBurger[k] + " ");
                }
                Thread.Sleep(3000);
                Console.Clear();
                beolvasSzoveg = Console.ReadLine();
            }
            if (beolvasSzoveg == aktualisBurger[i])
            {
                i++;
                Console.WriteLine("\n");
            }
            else
            {
                Console.WriteLine("Elbasztad");
            }
        }
        Console.WriteLine("Hurrá, Sikeresen teljesítetted a szintet!");
        szint++;
        g.kovetkezoSzint(szint);
    }
}
