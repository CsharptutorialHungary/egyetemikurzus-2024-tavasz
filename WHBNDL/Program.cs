using WHBNDL.UserInterface;

namespace WHBNDL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var ui = new UI(new CommandProvider(), new Host());
            ui.Run();
        }
    }
}
