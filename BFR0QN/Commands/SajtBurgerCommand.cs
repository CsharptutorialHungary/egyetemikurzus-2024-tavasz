using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFR0QN.Commands
{
    internal class SajtBurgerCommand
    {
        public string Name = "?sajtburger";

        public void Execute()
        {
            Console.WriteLine("zsemle, marhahúspogácsa, sajtszelet, uborka, hagyma, ketchup, mustár");
        }
    }
}
