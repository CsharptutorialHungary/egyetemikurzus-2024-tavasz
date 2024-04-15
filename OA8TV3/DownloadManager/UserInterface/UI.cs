using DownloadManager.Application;
using DownloadManager.Infrastructure;

namespace DownloadManager.UserInterface
{
    internal class UI
    {
        private readonly IHost _host;
        private readonly Controller _controller;
        private readonly ICommandLoader _commandLoader;

        public UI(IHost host, Controller controller, ICommandLoader commandLoader)
        {
            _host = host;
            _controller = controller;
            _commandLoader = commandLoader;
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