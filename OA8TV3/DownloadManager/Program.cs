using DownloadManager.Application;
using DownloadManager.UserInterface;

namespace DownloadManager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UI ui = new UI(new Host(), new Controller(), null);
            ui.Run();
        }
    }
}