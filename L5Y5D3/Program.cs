// See https://aka.ms/new-console-template for more information
using L5Y5D3;

Console.WriteLine("Szia, ebben az appban egyszerű matek feladatokat kell majd megoldanod, minden jó megoldásért 1 pont jár.\nMegoldásnak egész számokat írj be, osztásnál lefelé kerekíts, pl: 5/2=2  ");
Random rand = new Random();

List<Kerdes> kerdesek = new List<Kerdes>();
List<Valasz> valaszok = new List<Valasz>();

for (int i = 0; i < 10; i++)
{
    kerdesek.Add(new Kerdes(rand));
}


var rendezettkerdesek = from kerdes in kerdesek
                    orderby kerdes.Muvelet descending
                    select kerdes;



int helyesek = 0;
foreach (var kerdes in rendezettkerdesek){


    Console.WriteLine("Mennyivel egyenlő az alábbi egyenlet?");
    Console.Write($"({kerdes.A}) {kerdes.Muvelet} ({kerdes.B}) = ");
    for(int i = 0;i < 1; i++)
    {
        try
        {
            int valasz = Convert.ToInt32(Console.ReadLine());
            valaszok.Add(new Valasz(kerdes, valasz));
            if (valaszok.Last<Valasz>().Helyesseg)
            {
                Console.WriteLine("Helyes! :D");
                helyesek++;
            }
            else
            {
                Console.WriteLine("Menni fog ez még jobban...");
            }
        }
        catch (OverflowException)
        {
            Console.Error.WriteLine("Túl nagy szám, írd újra!");
            i--;
        }
        catch (FormatException)
        {
            Console.Error.WriteLine("Nem számot írtál be! Számot írj te troll!!");
            i--;
        }
        catch (Exception)
        {
            Console.Error.WriteLine("Te mi a bánatot csinálsz????");
            i--;
        }
    }
    
}
Console.WriteLine(helyesek +" helyes megoldásod volt.");

var maxvalasz = valaszok.MaxBy(i => i.Valaszod);
Console.WriteLine("A legnagyobb válaszod: " + maxvalasz.Valaszod);


string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),"valaszok.json");

using (var stream = File.Create(path))
{
    try
    {
        var serializer = new ValaszSerializer();
        await ValaszSerializer.SerializeToJson(stream, valaszok);
    }
    catch (IOException ex)
    {
        Console.WriteLine(ex.Message);
    }
}