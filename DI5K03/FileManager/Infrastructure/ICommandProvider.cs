namespace Filemanager.Infrastructure{
    internal interface ICommandProvider{
        ICommand[] Commands {get;}
    }
}