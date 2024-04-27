using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBNMY.Domain
{
    internal class ListEmptyException : Exception
    {
        public ListEmptyException(string message) : base(message) { }
    }
}
