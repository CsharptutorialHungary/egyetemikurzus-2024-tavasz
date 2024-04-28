// See https://aka.ms/new-console-template for more information
using L5Y5D3;

Console.WriteLine("Hello, World!");
Random rand = new Random();

List<Kerdes> kerdesek = new List<Kerdes>() ;
List<Valasz> valszok = new List<Valasz>() ;

for (int i = 0; i < 10; i++)
{
    kerdesek.Add(new Kerdes(rand));
    Console.WriteLine(kerdesek[i].ToString());
    Console.WriteLine("Mennyivel egyenlő az alábbi egyenlet?");
    Console.Write($"({kerdesek[i].A}) {kerdesek[i].Muvelet} ({kerdesek[i].B}) = ");
    int valasz = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine(new Valasz(kerdesek[i], valasz).Helyesseg);
}
