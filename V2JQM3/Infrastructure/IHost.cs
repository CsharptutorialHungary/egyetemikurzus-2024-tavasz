using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V2JQM3.Infrastructure
{
    internal interface IHost
    {
        string ReadLine();
        void WriteLine(string message);
        void Write(string message);
        void Exit();
        public void Help();
        public Task DownloadRssFile();
        public void DisplayLastRssFile();
        public void LoadSavedRssFiles();
        void ClearConsole() { Console.Clear(); }
        void EmptyDataFolder();
    }
}
