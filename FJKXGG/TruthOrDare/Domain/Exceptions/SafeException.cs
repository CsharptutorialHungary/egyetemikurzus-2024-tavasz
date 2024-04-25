using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruthOrDare.Domain.Exceptions
{
    /// <summary>
    /// Exception that is safe to show to the user. It is used to show a message to the user when something goes wrong.
    /// </summary>
    public class SafeException : Exception
    {
        public SafeException() : base("Something went wrong.") { }
        public SafeException(string message) : base(message) { }
    }
}
