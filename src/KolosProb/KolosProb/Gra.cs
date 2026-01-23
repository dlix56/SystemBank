using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;



namespace KolosProb
{
    public abstract class Gra : IComparable<Gra>
    {

        string tytul;
        DateTime datawydania;
        decimal cena;
        string kodProduktu;
        static int licznik;
        private bool czyPodpisanaPrzezAutora;

        public string Tytul { get => tytul; set => tytul = value; }
        public DateTime Datawydania { get => datawydania; set => datawydania = value; }
        public decimal Cena
        {
            get => cena; set
            {
                if (cena < 0)
                {
                    throw new ArgumentException("Dopłacanie za grę nie ma sensu");
                }
                else
                    cena = value;
            }
        }
        public string KodProduktu { get => kodProduktu; private set => kodProduktu = value; }
        public static int Licznik { get => licznik; set => licznik = value; }
        public bool CzyPodpisanaPrzezAutora { get => czyPodpisanaPrzezAutora; set => czyPodpisanaPrzezAutora = value; }

        public int CompareTo(Gra other)
        {
            if (other == null) return -1;
            return other.Cena.CompareTo(this.Cena);
        }

        static Gra()
        {
            licznik = 10;
        }

        public Gra()
        {
            tytul = "DEMO";
            datawydania = DateTime.Now;
            cena = 0;
            licznik++;
        }

        public Gra(string tytul, DateTime data, decimal cena) : this()
        {
            if (tytul == null || tytul.Length < 3)
            {
                throw new ArgumentException("Gra musi mieć tytuł!");
            }
            this.tytul = tytul;
            this.datawydania = data;
            this.cena = cena;

            kodProduktu = GenerujKod();
        }
        
       public string GenerujKod()
        {
            string litery = tytul.Substring(0, 3).ToUpper();
            return "GRA_" + litery + licznik;
        }

        public virtual double ObliczWartoscKolekcjonerska() => (double)cena * 0.9;

        public override string ToString()
        {
            return $"Gra: {kodProduktu} Osadnicy, cena: {cena} zł, data premiery: {datawydania}";
        }
    }
}
