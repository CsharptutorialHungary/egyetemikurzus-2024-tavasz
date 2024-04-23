using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFR0QN.Etelek
{
    public class Hamburger
    {
        public string Name { get; }
        public string Type { get; }
        public int Kcal { get; }
        public int Level { get; }
        public List<string> Ingredients { get; }

        public Hamburger(string name, string type, int kcal, int level, List<string> ingredients)
        {
            Name = name;
            Type = type;
            Kcal = kcal;
            Level = level;
            Ingredients = ingredients;
        }
    }
}
