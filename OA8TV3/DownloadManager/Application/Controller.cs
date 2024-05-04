using DownloadManager.Infrastructure;

namespace DownloadManager.Application
{
    internal class Controller
    {
        private readonly FileSystemManager _systemManager;
        private readonly Logger _logger;
        public Mode CurrentMode { get; set; }

        public Controller(FileSystemManager systemManager, Logger logger)
        {
            _systemManager = systemManager;
            _logger = logger;
            CurrentMode = Mode.Home;
        }
    }
}