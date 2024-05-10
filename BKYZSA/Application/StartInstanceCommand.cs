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
            await Console.Out.WriteLineAsync("Elindítottál egy párbeszédet a GPT-vel! Ha végeztél a beszélgetéssel" +
                "írd be, hogy \"exit\".\n");

            OpenAIAPI api = new();
            var chat = api.Chat.CreateConversation();

            chat.Model = Model.GPT4_Turbo;
            while (true)
            {
                Console.Out.WriteLine("[Felhasználó]");
                string? input = Console.ReadLine();
                if (input == null)
                    continue;
                if (input.Equals("exit"))
                    break;

                chat.AppendUserInput(input);

                Console.Out.WriteLine("[Modell]");
                await foreach (var res in chat.StreamResponseEnumerableFromChatbotAsync())
                {
                    Console.Write(res);
                }
                Console.Out.WriteLine("\n");
            }
            Ui.ModelRunning = false;

            // Conversation fájlba mentés 
            var messages = new List<MessageEntry>();
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                $"messages-{timestamp}.json");

            for (int i = 0; i < chat.Messages.Count; i++)
            {
                messages.Add(new MessageEntry
                {
                    // A beszélgetést mindig az user kezdi, ezért a páratlan üzeneteket az user küldi, a párosakat a modell
                    // i + 1, mert 0-tól indexelünk
                    Type = (i + 1) % 2 != 0 ? "User": "Assistant",
                    Message = chat.Messages[i].TextContent
                });
            }

            SaveToFile(path, messages);
        }

        public static void SaveToFile(string path, List<MessageEntry> messages)
        {
            using (var stream = File.Create(path))
            {
                try
                {
                    var serializer = new MessageEntrySerializer();
                    serializer.SerializeToJson(stream, messages);
                }
                catch (IOException ex)
                {
                    Console.WriteLine($"Hiba történt a beszélgetés elmentése során: {ex.Message}");
                }
            }
        }
    }
}
