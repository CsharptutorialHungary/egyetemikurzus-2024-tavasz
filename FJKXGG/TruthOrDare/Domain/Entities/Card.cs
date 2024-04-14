using System;
using System.Collections.Generic;

namespace TruthOrDare.Domain.Entities
{
    public record Card
    {
        public required int Id { get; init; }
        public string Text { get; init; }
        public GameMode GameMode { get; init; }

    }
}
