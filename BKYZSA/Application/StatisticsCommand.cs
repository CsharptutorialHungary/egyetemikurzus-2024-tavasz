using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using BKYZSA.Domain;
using BKYZSA.Infrastructure;

namespace BKYZSA.Application
{
    internal class StatisticsCommand : ICommand
    {
        public string Name => "statistics";

        public string Description => "A meglévő beszélgetések alapján lehet műveleteket végezni. paraméterek: " +
            "[top-usage (beszélgetések token használata csökkenő sorrendben), avg (átlagos token használat)]";
        // még agyalok, hogy mit lehetne még

        public void Execute(string[] args)
        {
            if(args.Length == 1)
            {
                Console.WriteLine("Használat: statistics top-usage|avg\n");
                return;
            }

            var files = FileUtility.GetFilesByPattern("messages-????-??-??_??-??-??.json");
            if (files == null)
            {
                Console.WriteLine("Még nincs elmentett beszélgetésed a modellel!\n");
                return;
            }

            List<Dialogue> dialogues = new();

            foreach (var file in files)
            {
                using (var stream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read))
                {
                    var dialogue = DialogueSerializer.DeserializeFromJson(stream);
                    if (dialogue != null)
                    {
                        dialogues.Add(dialogue);
                    }
                }
            }

            if (args[1].Equals("top-usage", StringComparison.CurrentCultureIgnoreCase))
            {
                int index = 1;
                foreach (var dialogue in dialogues.OrderByDescending(dialogue => dialogue.UsedToken))
                {
                    Console.WriteLine($"#{index++} - {dialogue.FileName} ({dialogue.UsedToken} tokens)\n");
                }
            }
            else if (args[1].Equals("avg", StringComparison.CurrentCultureIgnoreCase))
            {
                Console.WriteLine($"Átlagosan elhasznált mennyiség: " +
                    $"{dialogues.Average(dialogue => dialogue.UsedToken)} token\n"); 
            }
        }
    }
    
}
