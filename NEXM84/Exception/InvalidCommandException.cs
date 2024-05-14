using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEXM84.Exception
{
    internal class InvalidCommandException : NullReferenceException
    {
        public InvalidCommandException(string commandName) : base($"There is no such command \"{commandName}\"!\nType [help] for help!")
        {

        }
    }
}
