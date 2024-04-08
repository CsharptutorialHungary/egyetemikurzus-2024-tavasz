using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WHBNDL.Domain;
using WHBNDL.Infrastructure;

namespace WHBNDL.Application
{
    internal class HelpCommand : IShellCommand
    {
        public string Name => "help";

        public void Execute(IHost host, string[] args)
        {
            var textsDeserializer = new AppTextsDeserializer();
            Texts constTexts = AppTextsDeserializer.Deserialize();
            host.WriteLine(constTexts.Help);
        }
    }
}
