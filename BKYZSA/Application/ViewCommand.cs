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
            string pattern = "messages-????-??-??_??-??-??.json";

            DirectoryInfo directoryInfo = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));

            FileInfo[] files = directoryInfo.GetFiles(pattern);

            if (args.Length == 1)
            {
                Console.WriteLine("A visszanézhető beszélgetéseid:");
                if(files.Length == 0)
                {
                    Console.WriteLine("Még nincs visszanézhető beszélgetésed a modellel!");
                    return;
                }

                // TODO: listázza a beszélgetés jsonokat + vmi id
                for (int i = 0; i < files.Length; i++)
                {
                    Console.WriteLine($"[{i}] - {files[i].Name}");
                }
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
