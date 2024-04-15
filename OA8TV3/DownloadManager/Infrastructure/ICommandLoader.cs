namespace DownloadManager.Infrastructure
{
    internal interface ICommandLoader
    {
        ICommand[] Commands { get; }
    }
}