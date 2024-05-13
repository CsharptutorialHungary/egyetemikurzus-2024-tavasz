using WHBNDL.Database;
using WHBNDL.Domain;
using WHBNDL.Infrastructure;
using WHBNDL.UserInterface;
namespace WHBNDL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var database = new MemoryDatabase();
            var textsDeserializer = new AppTextsDeserializer();
            Texts constTexts = AppTextsDeserializer.Deserialize();
            Console.WriteLine(constTexts.Welcome);
            var ui = new UI(new CommandProvider(database), new Host());
            ui.Run();
            database.Close();
            Console.WriteLine("Database closed");
        }
    }
}
