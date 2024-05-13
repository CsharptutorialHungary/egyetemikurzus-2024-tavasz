namespace DownloadManager.Infrastructure
{
    internal interface ICommandLoader
    {
        ICommand[] Commands { get; }

        /// <summary>
        /// Visszaadja a parancsok listáját aszerint szűrve, hogy milyen módban van az alkalmazás.
        /// </summary>
        /// <param name="mode">Az alkalmazás aktuális módja</param>
        /// <returns>Elérhető parancsok listája</returns>
        ICommand[] GetCommandsByMode(Mode mode);
    }
}