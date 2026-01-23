using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KolosProb
{       
    public enum Gatunek
        {
                Strategiczna,
                Rodzinna,
                Imprezowa,
                Przygodowa
        }
    public class GraPlanszowa : Gra
    {
        Gatunek gatunek;
        public Gatunek Gatunek { get => gatunek; set => gatunek = value; }
        public GraPlanszowa() : base()
        {
            Gatunek = Gatunek.Rodzinna;
        }

        public GraPlanszowa(string tytul, DateTime data, decimal cena) : base(tytul, data, cena)
        {
            Gatunek = Gatunek;
        }

        public override double ObliczWartoscKolekcjonerska()
        {
            double Wartosc2 = base.ObliczWartoscKolekcjonerska();
            switch(Gatunek)
            {
                case Gatunek.Strategiczna:
                    return Wartosc2 * 1.5;
                case Gatunek.Imprezowa:
                    return Wartosc2 * 0.8;
                case Gatunek.Przygodowa:
                    return Wartosc2 * 2.5;
                case Gatunek.Rodzinna:
                    return Wartosc2 * 1.5;
                default:
                    return Wartosc2;
            }
            if(Datawydania.Year < 2000)
            {
                Wartosc2 = Wartosc2 * 2;
            }
        }
    }
}
