using XWG8AW.UserInterface;

namespace XWG8AW
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var ui = new Ui(new ReflectionCommandLoader(), new Host());
            ui.Run();
        }
    }
}
