using DownloadManager.Infrastructure;

namespace DownloadManager.UserInterface
{
    internal class UI
    {
        private readonly IHost _host;

        public UI(IHost host)
        {
            _host = host;
        }

        public void Run()
        {
            while (true)
            {
                _host.Write("Home: ");
                string input = _host.ReadLine();
                if (input == "exit")
                {
                    _host.Exit();
                }
            }
        }
    }
}