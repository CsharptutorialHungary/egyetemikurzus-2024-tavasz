using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
