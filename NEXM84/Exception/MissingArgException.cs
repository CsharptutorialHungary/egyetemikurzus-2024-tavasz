using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEXM84.Exception
{
    internal class MissingArgException : System.Exception
    {
        public MissingArgException(string commandName) : base($"Missing args while using \"{commandName}\"!\nType [help] for help!")
        {
            // You can add additional custom logic here if needed
        }
    }
}
