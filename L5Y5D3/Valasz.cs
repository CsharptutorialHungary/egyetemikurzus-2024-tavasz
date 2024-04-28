using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L5Y5D3
{
    internal sealed record class Valasz
    {
        public Kerdes Kerdesed { get; set; }
        public int Valaszod {  get; set; }

        public bool Helyesseg {  get; set; }

        
        public Valasz(Kerdes kerdes,int valasz)
        {
            Kerdesed = kerdes;
            Valaszod = valasz;

            Helyesseg = valasz == Kerdesed.Megoldas;
        }


    }
}
