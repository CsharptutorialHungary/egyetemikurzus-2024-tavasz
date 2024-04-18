namespace Filemanager.Infrastructure{
    internal interface IHost{
        string ReadL();
        
        void WriteLine(string toWrite);

        void Exit(string exitMsg);
    }
}