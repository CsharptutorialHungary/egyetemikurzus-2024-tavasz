using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKYZSA.Domain
{
    public record class Dialogue
    {
        public required List<MessageEntry> Messages { get; init; }
        public required int UsedToken { get; init; }
    }
}
