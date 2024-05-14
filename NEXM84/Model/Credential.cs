using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEXM84.Model
{
    public sealed record class Credential
    {
        public required string siteName { get; init; }
        public required string email { get; init; }
        public required string password { get; init; }
        public required string addDate { get; init; }
    }
}
