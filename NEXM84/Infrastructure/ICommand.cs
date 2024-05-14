namespace NEXM84.Infrastructure
{
    internal interface ICommand
    {
        string Name { get; }
        string Description { get; }

        void execute( IUiController iUiController, string[] args );
    }
}
