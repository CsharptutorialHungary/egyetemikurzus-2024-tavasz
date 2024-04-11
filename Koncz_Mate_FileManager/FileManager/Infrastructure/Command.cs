namespace Filemanager.Infrastructure{
    internal interface Command{
        string Name {get;}
        void Execute(Host host, string[] args);
    }
}