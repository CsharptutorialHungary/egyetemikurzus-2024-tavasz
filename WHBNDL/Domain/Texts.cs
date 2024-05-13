namespace WHBNDL.Domain
{
    public sealed record class Texts
    {
        public required string Welcome { get; set; }
        public required string Help { get; set; }
        public required string Description { get; set; }
        public required string End { get; set; }
    }
}
