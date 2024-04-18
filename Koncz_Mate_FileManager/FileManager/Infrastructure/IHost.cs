namespace Filemanager.Infrastructure{
    internal interface IHost{
        string ReadL();
        
        void WriteL(string toWrite);

        void Exit(string exitMsg);
    }
}