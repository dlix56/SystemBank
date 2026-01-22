using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankS.Bank.Wyjatki;

namespace BankS.Bank
{
    [Serializable]
    public delegate void OperacjaKonto(Konto konto, decimal kwota);

    public abstract class Konto : IComparable<Konto>
    {
        string nrkonta = string.Empty;
        decimal saldo;

        public event OperacjaKonto? OperacjaWykonana;

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
            OperacjaWykonana?.Invoke(this, kwota);
        }

        public virtual void Wyplac(decimal kwota)
        {
            if (kwota <= 0)
                throw new ArgumentException("Kwota musi być dodatnia");

            if (kwota > Saldo)
                throw new BrakFund();

            Saldo -= kwota;
            OperacjaWykonana?.Invoke(this, -kwota);
        }

        public abstract void Wyswietl();

        public int CompareTo(Konto? other)
        {
            if (other == null) return 1;
            return Saldo.CompareTo(other.Saldo);
        }
    }
}
