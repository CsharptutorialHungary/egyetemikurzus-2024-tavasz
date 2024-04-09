using DownloadManager.Infrastructure;

namespace DownloadManager.UserInterface
{
    internal class Host : IHost
    {
        public string ReadLine()
        {
            return Console.ReadLine() ?? throw new IOException("User input is empty");
        }

        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        public void Write(string message)
        {
            Console.Write(message);
        }

        public void Exit()
        {
            Environment.Exit(0);
        }
    }
}