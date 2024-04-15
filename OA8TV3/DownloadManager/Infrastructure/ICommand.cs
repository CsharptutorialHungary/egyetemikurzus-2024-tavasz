using DownloadManager.Application;

namespace DownloadManager.Infrastructure
{
    internal interface ICommand
    {
        string Name { get; }
        string Description { get; }
        Mode[] ValidModes { get; }

        void Execute(IHost host, Controller controller, Mode currentMode, string[] args);
    }
}