using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace KolosProb
{
    public class GryDoZagrania
    {
        string nazwa;
        Queue<Gra> kolejkaGier;

        public string Nazwa { get => nazwa; set => nazwa = value; }
        public Queue<Gra> KolejkaGier { get => kolejkaGier; set => kolejkaGier = value; }
          public GryDoZagrania(string nazwa)
        {
            kolejkaGier = new Queue<Gra>();
            Nazwa = nazwa;
        }

        public void DodajGre(Gra gra)
        {
            if (gra != null && gra.ObliczWartoscKolekcjonerska() > 50)
            { kolejkaGier.Enqueue(gra); }
        }

        public void ZagrajWKolejnaGre(Gra gra)
        {
            kolejkaGier.Dequeue();
        }
        
        public void WCoKolejnegoZagramy(Gra gra)
        {
            kolejkaGier.Peek();
        }

        public int GryPodpisane()
        {
            int count = 0;
            foreach (Gra gra in kolejkaGier)
            {
                if (gra.CzyPodpisanaPrzezAutora == true)
                {
                    count++;
                }
            }
            return count;
        }

        public void RaportWartosci()
        {
            double suma = 0;

            string raport =
                $"Kolekcja: {Nazwa}\n" +
                "Gry w kolejce:\n";

            foreach (Gra gra in kolejkaGier)
            {
                raport += gra.ToString() + "\n";
                suma += gra.ObliczWartoscKolekcjonerska();
            }

            raport += $"Sumaryczna wartość kolekcjonerska: {suma}";

            StringBuilder sb = new StringBuilder(raport);
            Console.WriteLine(sb.ToString());
        }

        public List<Gra> PobierzNajdrozszeGry(int ilosc)
        {
            List<Gra> lista = kolejkaGier.ToList();
            lista.Sort();
            return lista.Take(ilosc).ToList();

        }
        public static void Zapisz(string sciezka, GryDoZagrania obiekt)
        {
            XmlSerializer xs = new XmlSerializer(typeof(GryDoZagrania));
            FileStream fs = new FileStream(sciezka, FileMode.Create);
            xs.Serialize(fs, obiekt);
        }
    }
}