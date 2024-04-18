using Filemanager.Infrastructure;

namespace Filemanager{
    internal class Host : IHost
    {
        public void Exit(string exitMsg)
        {
            WriteLine(exitMsg);
            Environment.Exit(0);
        }

        public string ReadL()
        {
            return Console.ReadLine() ?? throw new Exception("enter a command");
        }

        public void WriteLine(string toWrite)
        {
            Console.WriteLine(toWrite);
        }
    }
}