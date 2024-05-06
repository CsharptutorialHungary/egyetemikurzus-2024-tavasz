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

        public string GetFolderName(string path)
        {
            return _systemManager.GetFolderName(path);
        }

        public bool AddRule(AbstractRule rule)
        {
            return _rules.Add(rule);
        }

        public IEnumerable<string> SearchRules()
        {
            return _rules.Select(rule =>
            {
                switch (rule)
                {
                    case ExtensionRule erule:
                        return $"Szabály .{erule.Extension} kiterjesztésre. Célmappa: {erule.Destination.FolderPath}";
                    case PatternRule prule:
                        return $"Szabály {prule.Pattern} keresett mintára. Célmappa: {prule.Destination.FolderPath}";
                    case SizeRule srule:
                        {
                            string comparisonType = srule.ComparisonType == 0 ? "minimális" : "maximális";
                            return
                                $"Szabály {comparisonType} fájlméretre, {srule.Value} bájt limittel. Célmappa: {srule.Destination.FolderPath}";
                        }
                    default:
                        return $"Egyéb szabály. Célmappa: {rule.Destination.FolderPath}";
                }
            });
        }

        public IEnumerable<string> SearchRules(string pattern)
        {
            var generalMatches = _rules.Select(rule => rule).Where(rule =>
                rule.Destination.FolderName.Contains(pattern) || rule.Destination.FolderPath.Contains(pattern));
            var specificMatches = _rules.Select(rule => rule).Where(rule =>
                (rule is ExtensionRule erule && erule.Extension == pattern) ||
                (rule is PatternRule prule && prule.Pattern.Contains(pattern)) ||
                (rule is SizeRule srule && (srule.ComparisonType == 0 ? pattern == "min" : pattern == "max")));
            return generalMatches.Concat(specificMatches).Distinct().Select(rule =>
            {
                switch (rule)
                {
                    case ExtensionRule erule:
                        return $"Szabály .{erule.Extension} kiterjesztésre. Célmappa: {erule.Destination.FolderPath}";
                    case PatternRule prule:
                        return $"Szabály {prule.Pattern} keresett mintára. Célmappa: {prule.Destination.FolderPath}";
                    case SizeRule srule:
                        {
                            string comparisonType = srule.ComparisonType == 0 ? "minimális" : "maximális";
                            return
                                $"Szabály {comparisonType} fájlméretre, {srule.Value} bájt limittel. Célmappa: {srule.Destination.FolderPath}";
                        }
                    default:
                        return $"Egyéb szabály. Célmappa: {rule.Destination.FolderPath}";
                }
            });
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