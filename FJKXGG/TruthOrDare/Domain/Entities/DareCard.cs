namespace TruthOrDare.Domain.Entities;
public sealed record DareCard(int Id, string Text, GameMode GameMode) : ICard;
