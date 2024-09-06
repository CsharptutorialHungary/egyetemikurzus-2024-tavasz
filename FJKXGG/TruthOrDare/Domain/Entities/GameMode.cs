namespace TruthOrDare.Domain.Entities;
// TODO: inconsistent property and constructor usage
public sealed record GameMode
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
}
