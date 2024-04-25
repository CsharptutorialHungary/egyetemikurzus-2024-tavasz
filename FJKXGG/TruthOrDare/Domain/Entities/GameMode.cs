namespace TruthOrDare.Domain.Entities
{
    public sealed record GameMode
    {
        public required int Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
    }
}
