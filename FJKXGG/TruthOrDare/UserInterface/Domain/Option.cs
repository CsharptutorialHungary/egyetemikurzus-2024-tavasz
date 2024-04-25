using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruthOrDare.UserInterface.Domain
{
    public sealed record Option
    {
        public required string Name { get; init; }
        public required object Description { get; init; }
        public required object Value { get; init; }
    }
}
