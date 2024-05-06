namespace Atomic
{
    internal class Program
    {
        static void Main(string[] args)
        {
            long counter = 0;
            Parallel.For(0, 1000, i =>
            {
                TimeSpan x = (DateTime.Now - DateTime.UtcNow);
                Interlocked.Increment(ref counter);
                //counter++;
                //--
                //+=
                //-=
                //&
                //|
            });
            Console.WriteLine($"counter == 1000: {counter == 1000}");
        }
    }
}
