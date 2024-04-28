using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L5Y5D3
{
    internal class Kerdes
    {
        public int A { get; }
        public int B { get; }
        public char Muvelet { get; }
        public int Megoldas { get; }



        public Kerdes(Random rand)
        {
            A = SzamGeneralas(rand);
            B = SzamGeneralas(rand);

            int veletlen = rand.Next(0, 4);

            Muvelet = MuveletGeneralas(veletlen);
            Megoldas = ValaszGeneralas(this.A, this.B, veletlen);

        }

        private int SzamGeneralas(Random r)
        {
            return r.Next(-100,100);
        }

        private char MuveletGeneralas(int muveletszam)
        { 
            switch (muveletszam)
            {
                case 0: return '+';
                case 1: return '-';
                case 2: return '*';
                case 3: return '/';
                default: return 'H';

            }
        }

        private int ValaszGeneralas(int elso, int masodik, int muveletszam)
        {
            switch (muveletszam) {
                case 0 : return elso + masodik;
                case 1: return elso - masodik;
                case 2: return elso * masodik;
                case 3: return elso / masodik;
                default: return 0;

            }
        }

        public override string? ToString()
        {
            return $"({A}) {Muvelet} ({B}) = {Megoldas}" ;
        }
    }
}
