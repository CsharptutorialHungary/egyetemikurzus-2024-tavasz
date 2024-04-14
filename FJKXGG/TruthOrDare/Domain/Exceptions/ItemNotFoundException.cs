using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruthOrDare.Domain.Exceptions
{
    public class ItemNotFoundException : PublicException
    {
        public ItemNotFoundException()
            : base("Item or items not found")
        {
        }
        public ItemNotFoundException(string message)
            : base(message)
        {
        }
        public ItemNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
