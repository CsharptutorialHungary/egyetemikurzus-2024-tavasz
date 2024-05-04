namespace DownloadManager.Infrastructure
{
    internal class Logger
    {
        private readonly string _logPath;

        public Logger(string logPath)
        {
            _logPath = logPath;
        }

        public void WriteToLog(string message)
        {
            File.AppendAllLinesAsync(Path.Combine(_logPath, "logs.txt"), [message]);
        }

        public IEnumerable<string> GetLog()
        {
            return File.ReadAllLines(Path.Combine(_logPath, "logs.txt"));
        }

        public void DeleteLog()
        {
            File.WriteAllTextAsync(Path.Combine(_logPath, "logs.txt"), "");
        }
    }
}