using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace V2JQM3.Infrastructure
{
    internal class RSSService : IRSSService
    {
        private IRSSFileManager _fileManager;

        public RSSService(IRSSFileManager fileManager)
        {
            _fileManager = fileManager;
            _fileManager.SetCurrentRssFilePath(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)?.Parent?.Parent?.Parent?.FullName);
        }
        public void DisplayLastRssFile()
        {
            string currentRssFilePath = _fileManager.GetCurrentRssFilePath();
            
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
                Console.WriteLine("No RSS file has been downloaded or selected yet.");
            }
        }

        public async void DownloadRssFile()
        {
            string currentRssFilePath = String.Empty;
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
    }
}
