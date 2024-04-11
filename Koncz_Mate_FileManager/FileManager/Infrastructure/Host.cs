namespace Filemanager.Infrastructure{
    internal interface Host{
        string ReadL();
        
        void WriteL(string toWrite);

        void Exit(string exitMsg);
    }
}