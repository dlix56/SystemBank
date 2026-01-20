using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankS.Bank
{
    public abstract class Konto
    {
        string nrkonta;
        decimal saldo;

        public string NrKonta
        {
            get => nrkonta; set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Nr konta nie może być pusty");
                nrkonta = value;
            }
        }

        public decimal Saldo
        {
            get => saldo;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Saldo nie może być ujemne");
                saldo = value;
            }
        }

        protected Konto(string nrkonta, decimal saldoStart)
        {
            NrKonta = nrkonta;
            Saldo = saldoStart;
        }

        public virtual void Wplac(decimal kwota)
        {
            if (kwota <= 0)
                throw new ArgumentException("Kwota musi byc dodatnia");

            Saldo += kwota;
        }

        public virtual void Wyplac(decimal kwota)
        {
            if (kwota <= 0)
                throw new ArgumentException("Kwota musi być dodatnia");

            if (kwota > Saldo)
                throw new ArgumentException("Brak środków");

            Saldo -= kwota;
        }

    }
}
