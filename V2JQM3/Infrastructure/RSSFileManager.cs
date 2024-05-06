using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace V2JQM3.Infrastructure
{
    internal class RSSFileManager : IRSSFileManager
    {
        private string _currentRssFilePath;
        public string GetCurrentRssFilePath() => _currentRssFilePath;
        public void SetCurrentRssFilePath(string path) => _currentRssFilePath = path;
        public void EmptySavedItemsFolder()
        {
            string executablePath = AppDomain.CurrentDomain.BaseDirectory;
            string projectRoot = Directory.GetParent(executablePath)?.Parent?.Parent?.Parent?.FullName;
            if (string.IsNullOrEmpty(projectRoot))
            {
                Console.WriteLine("Failed to determine the project root directory.");
                return;
            }
            string savedItemsFolder = Path.Combine(projectRoot, "SavedItems");

            try
            {
                DirectoryInfo di = new DirectoryInfo(savedItemsFolder);
                if (di.Exists)
                {
                    foreach (FileInfo file in di.GetFiles())
                    {
                        file.Delete();
                    }
                    Console.WriteLine("All files in the SavedItems folder have been deleted.");
                }
                else
                {
                    Console.WriteLine("SavedItems directory does not exist.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while trying to empty the SavedItems folder: {ex.Message}");
            }
        }

     

        public void LoadSavedRssFiles(List<RSSRecord> rssRecords)
        {
            string executablePath = AppDomain.CurrentDomain.BaseDirectory;
            string projectRoot = Directory.GetParent(executablePath)?.Parent?.Parent?.Parent?.FullName;
            if (string.IsNullOrEmpty(projectRoot))
            {
                Console.WriteLine("Failed to determine the project root directory.");
                return;
            }
            rssRecords.Clear();
            string savedItemsPath = Path.Combine(projectRoot, "SavedItems");
            List<RSSRecord> records = new List<RSSRecord>();
            DirectoryInfo di = new DirectoryInfo(savedItemsPath);

            if (!di.Exists)
            {
                Console.WriteLine("SavedItems directory does not exist.");
                return;
            }

            foreach (FileInfo file in di.GetFiles("*.xml"))
            {
                try
                {
                    XDocument doc = XDocument.Load(file.FullName);
                    var titleNode = doc.Descendants("channel").Elements("title").FirstOrDefault();
                    string title = titleNode != null ? titleNode.Value : "No title";
                    string filename = file.Name;
                    DateTime date = DateTime.ParseExact(filename.Substring(4, 14), "yyyyMMddHHmmss", null);

                    records.Add(new RSSRecord(filename, title, date, file.FullName));
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error processing file {file.Name}: {e.Message}");
                }
            }
            if (records.Count == 0) { Console.WriteLine("There are no files downloaded yet."); return; }
            //LINQ
            var sortedRecords = records.OrderByDescending(r => r.Date).ToList();
            int index = 1;
            //Records listing
            foreach (var record in sortedRecords)
            {
                Console.WriteLine($"{index}. Date: {record.Date}, Title: {record.Title}, Filename: {record.Filename}");
                index++;
            }
            Console.WriteLine("Enter the number of the RSS file to load or type 'back' to return:");
            while (true)
            {
                string input = Console.ReadLine();
                if (input.ToLower() == "back")
                    return;

                if (int.TryParse(input, out int selectedIndex) && selectedIndex > 0 && selectedIndex <= sortedRecords.Count)
                {
                    _currentRssFilePath = sortedRecords[selectedIndex - 1].FullPath;
                    Console.WriteLine("RSS file loaded successfully.");
                    return;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please try again.");
                }
            }
        }

    }
}
