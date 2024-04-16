namespace BFR0QN;

class Program
{
    static void Main(string[] args)
    {
        GameManager g = new GameManager();
        g.Betolt();
        Console.WriteLine("Anita - Szeretnék kérni egy Sajtburgert!");
        //Sajtburger: zsemle, marhahúspogácsa, sajtszelet, uborka, hagyma, ketchup, mustár
        string beolvasSzoveg=Console.ReadLine();
        String[] sajturger = ["zsemle", "marhahúspogácsa", "sajtszelet", "uborka", "hagyma", "ketchup", "mustár"];
        if(beolvasSzoveg == ("?sajtburger"))
        {
            Console.WriteLine("zsemle, marhahúspogácsa, sajtszelet, uborka, hagyma, ketchup, mustár");
           
            Thread.Sleep(3000);
            Console.Clear();
        }
        for (int i = 0; i < sajturger.Length; i++)
        {
            Console.Write("{0}. elem:",i+1);
            string aktualisElem = Console.ReadLine();
            if (aktualisElem != sajturger[i])
            {
                Console.WriteLine("Elbasztad");
                i = 0;
            }
            Console.WriteLine("\n");
        }
        Console.WriteLine("Hurrá! Sikeresen teljesítetted a szintet!");
        Console.ReadKey();
    }
}
