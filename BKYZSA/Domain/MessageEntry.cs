using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKYZSA.Domain
{

    public record class MessageEntry
    {
        public required string Type { get; init; }
        public required string Message { get; init; }

    }
}
