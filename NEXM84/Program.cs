using NEXM84.UserInterface;

internal class Program
{
    private static void Main(string[] args)
    {
        var mainInterface = new Application( new UiController() );

        DriveInfo[] allDrives = DriveInfo.GetDrives();

        mainInterface.Launch();
    }
}

