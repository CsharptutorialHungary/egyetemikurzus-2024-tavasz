//Gulag sort
//Stalin sort
//Bogo sort
//WTF Sort

namespace TaskDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] tomb = new int[20];
            for (int i=0; i<tomb.Length; i++)
            {
                tomb[i] = Random.Shared.Next(0, 100);
            }
            for (int j = 0; j<19; j++)
            {
                try
                {
                    int copy = j;
                    Task.Run(() =>
                    {
                        Print(tomb[copy]);
                    });
                }
                catch (Exception) { }
            }

            Console.ReadKey();
        }

        private static async Task Print(int j)
        {
            await Task.Delay(j * 10);
            Console.WriteLine(j);
        }
    }
}
