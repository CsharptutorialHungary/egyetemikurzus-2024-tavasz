using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKYZSA.Commands
{
    internal class StartInstanceCommand : ICommand
    {
        public string Name => "startinstance";

        public string Description => "Elindít egy beszélgetést a specifikált GPT modellel. (gpt-4t, gpt-3.5t)";

        public void Execute(string[] args)
        {
            throw new NotImplementedException();
        }
    }
}
