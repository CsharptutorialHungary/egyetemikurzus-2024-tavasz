namespace TruthOrDare.Domain.Entities;

public sealed record TruthCard(int Id, string Text, GameMode GameMode) : Card(Id, Text, GameMode), ICard;
