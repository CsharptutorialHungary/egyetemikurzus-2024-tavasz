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
        /// <summary>
        /// Tartalmazza, hogy milyen argumentumokkal hívható meg a parancs.
        /// </summary>
        string[] ValidArguments { get; }

        void Execute(IHost host, Controller controller, ICommandLoader commandLoader, string[] args);
    }
}