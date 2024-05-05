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
        static string currentRssFilePath = string.Empty;
        static List<RSSRecord> rssRecords = new List<RSSRecord>();
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
            Console.WriteLine("Available commands:");
            Console.WriteLine("download - Download RSS file");
            Console.WriteLine("display - Display last RSS file");
            Console.WriteLine("load - Load saved RSS files");
            Console.WriteLine("clear - Clears the console");
            Console.WriteLine("empty - Deletes the already downloaded RSS xml(s)");
            Console.WriteLine("help - Display commands");
            Console.WriteLine("exit - Exit");
            Console.WriteLine("---------------------------------------------------");
        }

        public void LoadSavedRssFiles()
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
            if (records.Count == 0) {Console.WriteLine("There are no files downloaded yet.");return;}
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
                    currentRssFilePath = sortedRecords[selectedIndex - 1].FullPath;
                    Console.WriteLine("RSS file loaded successfully.");
                    return;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please try again.");
                }
            }

        }

        public void DisplayLastRssFile()
        {
            if (!string.IsNullOrEmpty(currentRssFilePath) && File.Exists(currentRssFilePath))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(currentRssFilePath);
                XmlNodeList items = doc.SelectNodes("//item");
                if (items.Count == 0)
                {
                    Console.WriteLine("No items found in the RSS file.");
                    return;
                }
                int index = 1;
                foreach (XmlNode item in items)
                {
                    XmlNode titleNode = item.SelectSingleNode("title");
                    if (titleNode != null)
                    {
                        Console.WriteLine($"{index}. {titleNode.InnerText}");
                        index++;
                    }
                }

                while (true)
                {
                    Console.Write("Enter the number of the article to open, type 'back' to return to the main menu: ");
                    string input = Console.ReadLine().Trim().ToLower();

                    if (input == "back")
                    {
                        break;
                    }
                    else
                    {
                        int choice;
                        if (int.TryParse(input, out choice) && choice > 0 && choice <= items.Count)
                        {
                            XmlNode linkNode = items[choice - 1].SelectSingleNode("link");
                            if (linkNode != null)
                            {
                                Process.Start(new ProcessStartInfo(linkNode.InnerText) { UseShellExecute = true });
                                Console.WriteLine("Article opened.");
                            }
                            else
                            {
                                Console.WriteLine("No URL found for the selected article.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid choice, please try again.");
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("No RSS file has been downloaded yet.");
            }
        }


        public async Task DownloadRssFile()
        {
            Console.Write("Enter RSS feed URL: ");
            string url = Console.ReadLine();
            using (var client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        string executablePath = AppDomain.CurrentDomain.BaseDirectory;
                        string projectRoot = Directory.GetParent(executablePath)?.Parent?.Parent?.Parent?.FullName;
                        if (projectRoot != null)
                        {
                            string savedItemsFolder = Path.Combine(projectRoot, "SavedItems");
                            Directory.CreateDirectory(savedItemsFolder);
                            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                            string fileName = $"RSS_{timestamp}.xml";
                            currentRssFilePath = Path.Combine(savedItemsFolder, fileName);
                            await File.WriteAllTextAsync(currentRssFilePath, content);
                            Console.WriteLine($"RSS file downloaded and saved successfully at:\n{currentRssFilePath}");
                            return;
                        }
                        else
                        {
                            Console.WriteLine("Couldn't find SavedItems folder!");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Failed to download the RSS file. Status code: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }
        }
        public void EmptyDataFolder()
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

    }
}
