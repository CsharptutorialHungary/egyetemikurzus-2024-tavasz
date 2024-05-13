namespace Filemanager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UI ui = new(new CommandProvider(), new Host());
            ui.RunAsync().Wait();
        }
    }
}