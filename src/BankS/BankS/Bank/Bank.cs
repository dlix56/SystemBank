using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using System.Linq;

namespace BankS.Bank
{
    public class Bank
    {
        private List<Konto> konta = new();

        public IReadOnlyList<Konto> Konta => konta;

        public void AddKonto(Konto konto)
        {
            if (konto == null)
            {
                throw new ArgumentNullException(nameof(konto));
            }
            konta.Add(konto);
        }

        public Konto ZnajdzNumer(string nrKonta)
        {
            var konto = konta.FirstOrDefault(k => k.NrKonta == nrKonta);

            if (konto == null)
                throw new InvalidOperationException("Nie znaleziono konta");

            return konto;
        }

        public void WyswietlAll()
        {
            foreach (var konto in konta)
            {
                konto.Wyswietl();
            }
        }

        public void ZapiszDoPliku(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Konto>));
            using FileStream fs = new FileStream(path, FileMode.Create);
            serializer.Serialize(fs, konta);
        }

        public void WczytajZPliku(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Konto>));
            using FileStream fs = new FileStream(path, FileMode.Open);
            konta = (List<Konto>)serializer.Deserialize(fs)!;
        }

        public IEnumerable<Konto> PobierzSortowane()
        {
            return konta.OrderBy(k => k.Saldo);
        }
    }
}
