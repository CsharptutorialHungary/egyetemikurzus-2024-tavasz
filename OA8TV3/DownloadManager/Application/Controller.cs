using DownloadManager.Infrastructure;

namespace DownloadManager.Application
{
    internal class Controller
    {
        public Logger LogManager { get; }
        public Mode CurrentMode { get; set; }

        public Controller(Logger logger)
        {
            LogManager = logger;
            CurrentMode = Mode.Home;

            LogManager.DeleteLog();
        }
    }
}