using DownloadManager.Infrastructure;

namespace DownloadManager.Application
{
    internal class FileSystemManager
    {
        private readonly string _sourcePath;
        private readonly Logger _logger;

        public FileSystemManager(string sourcePath, Logger logger)
        {
            _sourcePath = sourcePath;
            _logger = logger;
        }

        public IEnumerable<string> GetSourceFolder()
        {
            return CheckPath(_sourcePath) ? Directory.GetFiles(_sourcePath).Select(file => file)
                .Where(file => (File.GetAttributes(file) & FileAttributes.Hidden) == 0) : [];
        }

        public string GetExtension(string path)
        {
            return Path.GetExtension(path);
        }

        public long GetSize(string path)
        {
            return new FileInfo(path).Length;
        }

        public string GetName(string path)
        {
            return Path.GetFileNameWithoutExtension(path);
        }

        public bool CheckPath(string path)
        {
            return Directory.Exists(path);
        }

        public bool MoveFile(string from, string to)
        {
            if (!CheckPath(to))
            {
                return false;
            }
            string fileName = new FileInfo(from).Name;
            File.Move(from, Path.Combine(to, fileName));
            return true;
        }
    }
}