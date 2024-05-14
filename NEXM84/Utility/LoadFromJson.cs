using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using NEXM84.Infrastructure;
using NEXM84.Model;

namespace NEXM84.Utility
{
    internal class LoadFromJson()
    {
        public static async Task<List<Credential>> LoadCredentialsFromJson(string filePath, IUiController uiController)
        {
            try
            {
                string jsonContent = await File.ReadAllTextAsync(filePath);
                List<Credential> credentials = null;

                await Task.Run(() =>
                {
                    credentials = JsonSerializer.Deserialize<List<Credential>>(jsonContent);

                    var sortedCredentials = from credential in credentials
                                            orderby credential.siteName ascending
                                            select credential;
                    credentials = sortedCredentials.ToList();//REMELEM IGY JO
                    int i = 0;
                    uiController.writeLine("");
                    foreach (Credential credential in credentials)
                    {
                        uiController.writeLine(i+" | "+$"Site: {credential.siteName}, Email: {credential.email}, Password: {credential.password}, Date: {credential.addDate}");
                        i++;
                    }
                });
                uiController.GetFileBrowser().setCredentialToObservePool(credentials);
                return credentials;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"An error occurred while loading credentials from JSON: {ex.Message}");
                return null;
            }
        }
    }
}
