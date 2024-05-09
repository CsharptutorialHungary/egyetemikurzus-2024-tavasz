using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BKYZSA.UserInterface;

using OpenAI_API;
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

            string input = "";

            while (!input.Equals("exit"))
            {
                Console.Out.Write("Felhasználó >> ");
                input = Console.ReadLine();
                if(input == null)
                    continue;

                chat.AppendUserInput(input);

                Console.Out.Write("Modell << ");
                await foreach (var res in chat.StreamResponseEnumerableFromChatbotAsync())
                {
                    Console.Write(res);
                }
                Console.Out.WriteLine("\n");
            }
            Ui.ModelRunning = false;

            //TODO: conversation fájlba mentés 
        }
    }
}
