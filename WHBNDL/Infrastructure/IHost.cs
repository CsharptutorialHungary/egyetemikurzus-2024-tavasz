namespace WHBNDL.Infrastructure
{
    internal interface IHost
    {
        string ReadLine();
        void WriteLine(string message);
        void Exit();
    }
}
