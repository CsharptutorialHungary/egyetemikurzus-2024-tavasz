using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BKYZSA.Domain;
using BKYZSA.Infrastructure;

namespace BKYZSA.Application
{
    internal class ViewCommand : ICommand
    {
        public string Name => "view";

        public string Description => "Visszatekintheted az eddigi beszélgetéseid.";

        public void Execute(string[] args)
        {
            var files = FileUtility.GetFilesByPattern("messages-????-??-??_??-??-??.json");

            if(files == null)
            {
                Console.WriteLine("Még nincs visszanézhető beszélgetésed a modellel!");
                return;
            } 

            if (args.Length == 1)
            {
                FileUtility.ListFiles(files);
                Console.WriteLine("A visszanézéshez használd így a \"view\" parancsot: view <id>");
            }
            else
            {
                int index = Convert.ToInt32(args[1]);
                PrintMessages(files, index);
            }
        }

        public static void PrintMessages(FileInfo[] files, int index)
        {
            using (var stream = new FileStream(files[index].FullName, FileMode.Open, FileAccess.Read))
            {
                var dialogue = DialogueSerializer.DeserializeFromJson(stream);
                if(dialogue == null)
                {
                    Console.WriteLine("A párbeszéd megnyitása nem sikerült.");
                    return;
                }

                foreach(var message in dialogue.Messages)
                {
                    Console.WriteLine($"[{message.Sender}]");
                    Console.WriteLine(message.Message + "\n");
                }

            }
        }
    }
}
