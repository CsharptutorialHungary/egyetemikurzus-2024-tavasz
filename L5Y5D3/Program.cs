// See https://aka.ms/new-console-template for more information
using L5Y5D3;

Console.WriteLine("Hello, World!");
Random rand = new Random();

List<Kerdes> kerdesek = new List<Kerdes>() ;

for (int i = 0; i < 10; i++)
{
    kerdesek.Add(new Kerdes(rand));
    Console.WriteLine(kerdesek[i].ToString());
}
