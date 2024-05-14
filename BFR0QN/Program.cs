using System;
using System.Security.Cryptography;

namespace BFR0QN;

class Program
{
    static async Task Main(string[] args)
    {

        GameManager gameManager = GameManager.Instance;
        Console.WriteLine(gameManager.AvarageFood());
        await gameManager.Load();
        
        int level = gameManager.Level;
        string readConsole="";
        while (readConsole != "0")
        {
            Food currentFood = gameManager.NextLevel(level);
           String[] currentFoodToString = currentFood.Ingredients.ToArray();
            int i = 0;
            Console.Clear();
            Console.WriteLine("Anita - Szeretnék kérni egy " + currentFood.Name);
            Console.WriteLine("hozzávalok: ");
            gameManager.Help(currentFoodToString);
            while (i < currentFoodToString.Length)
            {
                Console.Write("{0}. elem:", i + 1);
                readConsole = Console.ReadLine();
                if (readConsole == ("?etel"))
                {
                    gameManager.Help(currentFoodToString);
                }
                else if (readConsole == currentFoodToString[i])
                {
                    i++;
                    Console.WriteLine("\n");
                }
                else if (readConsole == "mentes")
                {
                    Console.Write("Mentés neve: ");
                    string saveName = Console.ReadLine();
                    if (gameManager.IsSave(saveName,level))
                    {
                        Console.WriteLine("Sikeres mentés");
                        gameManager.CreateJsonFileToSaveTheGame();
                    }
                    else
                    {
                        Console.WriteLine("Sikertelen mentés");
                    }
                }
                else
                {
                    Console.WriteLine("Nem teljesen! Próbáld újra. Ha kell segítség, írd be, hogy ?etel");
                }
            }
            Console.Clear();
            Console.WriteLine("Hurrá, Sikeresen teljesítetted a "+level+". szintet!\n Jöhet a kövi?");
            Console.ReadKey();
            level++;
        }
    }
}
