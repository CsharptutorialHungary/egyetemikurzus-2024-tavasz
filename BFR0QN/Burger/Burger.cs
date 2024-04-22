using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFR0QN.Burger
{
    internal class Burger
    {
        public string Name { get; set; }
        public int Kcal { get; set; }
        public int Level { get; set; }
        public List<string> Ingredients { get; set; }
    }
}
