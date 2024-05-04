using DownloadManager.Infrastructure;

namespace DownloadManager.Application
{
    internal class Controller
    {
        public Mode CurrentMode { get; set; }

        public Controller()
        {
            CurrentMode = Mode.Home;
        }
    }
}