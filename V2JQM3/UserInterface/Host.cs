using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using V2JQM3.Infrastructure;

namespace V2JQM3.UserInterface
{
    internal class Host:IHost
    {
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
            Console.WriteLine("Available options:");
            Console.WriteLine("download - Download RSS file");
            Console.WriteLine("display - Display last RSS file");
            Console.WriteLine("load - Load saved RSS files");
            Console.WriteLine("help - Display commands");
            Console.WriteLine("exit - Exit");
        }

        public void LoadSavedRssFiles()
        {
            throw new NotImplementedException();
        }
        public void DisplayLastRssFile()
        {
            throw new NotImplementedException();
        }

        public Task DownloadRssFile()
        {
            throw new NotImplementedException();
        }

    }
}
