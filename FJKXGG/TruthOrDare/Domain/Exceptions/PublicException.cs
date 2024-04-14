using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruthOrDare.Domain.Exceptions
{
    public class PublicException : Exception
    {
        public PublicException() { }
        public PublicException(string message) : base(message) { }
        public PublicException(string message, Exception inner) : base(message, inner) { }
    }
}
