namespace Filemanager.Infrastructure{
    internal interface IHost{
        string ReadLine();
        
        void WriteLine(string toWrite);

        void Exit(string exitMsg);
    }
}