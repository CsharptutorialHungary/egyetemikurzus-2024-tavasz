namespace TruthOrDare.Domain.Entities
{
    public interface ICard
    {
        GameMode GameMode { get; init; }
        int Id { get; init; }
        string Text { get; init; }
    }
}