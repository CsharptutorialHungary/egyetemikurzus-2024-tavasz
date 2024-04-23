using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFR0QN.Burger
{
    public class Hamburger
    {
        public string Name { get; }
        public int Kcal { get; }
        public int Level { get; }
        public List<string> Ingredients { get; }

        public Hamburger(string name, int kcal, int level, List<string> ingredients)
        {
            Name = name;
            Kcal = kcal;
            Level = level;
            Ingredients = ingredients;
        }
    }
}
