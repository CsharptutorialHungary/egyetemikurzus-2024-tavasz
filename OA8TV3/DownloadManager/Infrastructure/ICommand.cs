using DownloadManager.Application;

namespace DownloadManager.Infrastructure
{
    internal interface ICommand
    {
        string Name { get; }
        string Description { get; }
        /// <summary>
        /// Tartalmazza azoknak a módoknak a listáját, amelyekben végrehajtható a parancs.
        /// Üres lista esetén bármely módban végrehajtható.
        /// </summary>
        Mode[] ValidModes { get; }

        void Execute(IHost host, Controller controller, Mode currentMode, string[] args);
    }
}