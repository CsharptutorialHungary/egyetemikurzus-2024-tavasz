using BKYZSA.Commands;
using BKYZSA.UserInterface;

internal class Program
{
    private static void Main(string[] args)
    {
        var ui = new Ui(new CommandLoader());
        ui.Run();
    }
}