using WHBNDL.Domain;
using WHBNDL.UserInterface;
using WHBNDL.Infrastructure;
namespace WHBNDL
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var textsDeserializer = new AppTextsDeserializer();
            Texts constTexts = AppTextsDeserializer.Deserialize();
            Console.WriteLine(constTexts.Welcome);


            var ui = new UI(new CommandProvider(), new Host());
            ui.Run();
        }
    }
}
