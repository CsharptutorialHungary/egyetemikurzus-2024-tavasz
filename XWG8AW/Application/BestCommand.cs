using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XWG8AW.Infrastructure;

namespace XWG8AW.Application
{
    internal class BestCommand : IShellCommand
    {
        public string Name => "best";

        public void Execute(IHost host, string[] args)
        {
            host.WriteLine("Adja meg a játékos nevét!");

            LinqController controller = new LinqController();

            string user = host.ReadLine();

            var bestScore = controller.BestScore(user);

            host.WriteLine(bestScore.Result);
        }
    }
}
