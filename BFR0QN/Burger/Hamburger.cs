using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFR0QN.Burger
{
    internal class Hamburger
    {
        private string _name;
        private int _kcal;
        private string[] elemek;
        int elemSzamlalo;
        public Hamburger(int elemSzam,string nev) {
            elemek=new string[elemSzam];
            _name = nev;
        }
        public void Add(string elem)
        {
            if(elemSzamlalo < elemek.Length)
            {
                elemek[elemSzamlalo++] = elem;
            }
            else
            {
                Console.WriteLine("nem fér be több");
            }
        }
        public String nev()
        {
            return _name;
        }
        public int Lenght()
        {
            return elemSzamlalo;
        }
        public String[] tomb()
        {
            String[] tombocske=new String[elemSzamlalo];
            for (int i = 0; i < elemSzamlalo; i++)
            {
                tombocske[i] = elemek[i];
            }
            return tombocske;
        }
    }
}
