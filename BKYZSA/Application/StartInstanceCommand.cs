using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BKYZSA.Domain;
using BKYZSA.Infrastructure;
using BKYZSA.UserInterface;

using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;

namespace BKYZSA.Commands
{
    internal class StartInstanceCommand : ICommand
    {
        public string Name => "startinstance";

        // maybe válaszható modell, ha marad idő
        public string Description => "Elindít egy beszélgetést GPT-4 Turbo modellel.";

        public async void Execute(string[] args)
        {
            Console.WriteLine("Elindítottál egy párbeszédet a GPT-vel! Ha végeztél a beszélgetéssel" +
                "írd be, hogy \"exit\".\n");

            OpenAIAPI api = new();
            var chat = api.Chat.CreateConversation();
            chat.Model = Model.GPT4_Turbo;

            while (true)
            {
                Console.WriteLine("[User]");
                string? input = Console.ReadLine();
                if (input == null)
                    continue;
                if (input.Equals("exit"))
                    break;

                chat.AppendUserInput(input);

                Console.WriteLine("[Assistant]");
                try
                {
                    Console.Write(await chat.GetResponseFromChatbotAsync());
                    Console.WriteLine("\n");
                } catch (System.Security.Authentication.AuthenticationException ex)
                {
                    Console.WriteLine(ex.Message + "\n");
                    Ui.ModelRunning = false;
                    return;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Hiba történt: " + ex.Message + "\n");
                }
                Console.WriteLine(chat.MostRecentApiResult.Usage.TotalTokens);
            }
            Ui.ModelRunning = false;

            // Conversation fájlba mentés 
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            string filename = $"messages-{timestamp}.json";
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                filename);

            int index = 1;

            var messages = chat.Messages.Select(entry => new MessageEntry 
            {
                // A beszélgetést mindig az user kezdi, ezért a páratlan üzeneteket az user küldi, a párosakat a modell
                Sender = index++ % 2 == 0 ? "Assistant" : "User",
                Message = entry.TextContent
            }).ToList();

            var dialogue = new Dialogue {
                FileName = filename,
                Messages = messages,
                UsedToken = chat.MostRecentApiResult.Usage.TotalTokens
            };

            SaveToFile(path, dialogue);
        }

        public static async void SaveToFile(string path, Dialogue dialogue)
        {
            using (var stream = File.Create(path))
            {
                try
                {
                    await DialogueSerializer.SerializeToJson(stream, dialogue);
                }
                catch (IOException ex)
                {
                    Console.WriteLine($"Hiba történt a beszélgetés elmentése során: {ex.Message}");
                }
            }
        }
    }
}
