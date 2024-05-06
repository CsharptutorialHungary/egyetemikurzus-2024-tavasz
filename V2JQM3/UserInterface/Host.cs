using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

using V2JQM3.Infrastructure;

namespace V2JQM3.UserInterface
{
    internal class Host : IHost
    {
        
        static List<RSSRecord> rssRecords = new List<RSSRecord>();
        IRSSFileManager fileManager;
        IRSSService rssService;

        public Host() {
            fileManager = new RSSFileManager();
            rssService = new RSSService(fileManager);
        }

        public string ReadLine()
           => Console.ReadLine() ?? throw new InvalidOperationException();

        public void Write(string message)
        {
            Console.Write(message);
        }
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
        public void Exit()
        {
            Environment.Exit(0);
        }

        public void Help()
        {
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine(
                "RSS-Downloader 1.0:\n" +
                "You can download RSS xml files with this console app, they get stored locally in a folder.\n" +
                "When you start the application you need to load a file before you can display it.\n" +
                "There are 4 xmls included in the app for testing."
                );
            Console.WriteLine("Available commands:");
            Console.WriteLine("download - Download RSS file");
            Console.WriteLine("display - Display last RSS file");
            Console.WriteLine("load - Load and pick saved RSS files");
            Console.WriteLine("clear - Clears the console");
            Console.WriteLine("empty - Deletes the already downloaded RSS xml(s)");
            Console.WriteLine("help - Display commands");
            Console.WriteLine("exit - Exit");
            Console.WriteLine("---------------------------------------------------");
        }

        public void LoadSavedRssFiles()
        {
           fileManager.LoadSavedRssFiles(rssRecords);
        }

        public void DisplayLastRssFile()
        {
            rssService.DisplayLastRssFile();
        }

        public void DownloadRssFile()
        {
            rssService.DownloadRssFile();
        }

        public void EmptyDataFolder()
        {
            fileManager.EmptySavedItemsFolder();
        }

    }
}
