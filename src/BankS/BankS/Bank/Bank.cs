using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
