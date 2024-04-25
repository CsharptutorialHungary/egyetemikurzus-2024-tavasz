namespace TruthOrDare.Domain.Entities;
/// <summary>
/// Represents a card in the game.
/// </summary>
public interface ICard
{
    GameMode GameMode { get; init; }
    int Id { get; init; }
    string Text { get; init; }
}