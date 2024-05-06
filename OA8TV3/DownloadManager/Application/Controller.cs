using DownloadManager.Domain;
using DownloadManager.Infrastructure;

namespace DownloadManager.Application
{
    internal class Controller
    {
        private readonly FileSystemManager _systemManager;
        private readonly RuleSerializer _serializer;
        private readonly Logger _logger;
        private HashSet<AbstractRule> _rules { get; }
        public Mode CurrentMode { get; set; }

        public Controller(FileSystemManager systemManager, RuleSerializer serializer, Logger logger)
        {
            _systemManager = systemManager;
            _serializer = serializer;
            _logger = logger;
            _rules = new HashSet<AbstractRule>(_serializer.DeserializeFromJson().Result);
            CurrentMode = Mode.Home;
        }

        public bool ValidPath(string path)
        {
            return _systemManager.CheckPath(path);
        }

        public bool AddRule(AbstractRule rule)
        {
            return _rules.Add(rule);
        }

        public void SaveRules()
        {
            Task.Run(() => _serializer.SerializeToJson(_rules.ToArray()));
        }

        public IEnumerable<string> SearchLogs()
        {
            return _logger.GetLog();
        }

        public IEnumerable<string> SearchLogs(string pattern)
        {
            return _logger.GetLog().Select(row => row).Where(row => row.Contains(pattern));
        }

        public void DeleteLogs()
        {
            _logger.DeleteLog();
        }
    }
}