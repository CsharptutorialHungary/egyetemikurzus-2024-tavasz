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

            LinqController linqController = new LinqController();

            string user = host.ReadLine();

            var bestScore = linqController.BestScoreByPlayer(user);

            host.WriteLine(bestScore.Result+"\n");
        }
    }
}
