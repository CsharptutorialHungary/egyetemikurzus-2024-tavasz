namespace HighlanderProgram
{
    internal class Program
    {
        private const string MutexName = "46643EA6-90DB-40FD-B628-E28C6927EF74";

        static void Main(string[] args)
        {
            if (Mutex.TryOpenExisting(MutexName, out var mutex))
            {
                //Akkor már fut egy példány
                Console.WriteLine("Már fut egy példány");
                Console.ReadKey();
                Environment.Exit(-1);
            }

            using var newMutex = new Mutex(false, MutexName);
            Console.WriteLine("Hello, World!");
            Console.ReadKey();
        }
    }
}
