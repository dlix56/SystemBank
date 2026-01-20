using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankS.Bank
{
    public class KontoOszczednosc : Konto
    {
        decimal procent;

        public decimal Procent { get; set; }
    
        public KontoOszczednosc(string nrkonta, decimal procent, decimal saldoStart)
            : base(nrkonta, saldoStart)
        {
            if (procent < 0)
            {
                throw new ArgumentException("Oprocentowanie nie jest ujemne");
            }
            this.procent = procent;
        }

        public override void Wyswietl()
        {
            Console.WriteLine($"Konto oszczędnościowe: {NrKonta}, saldo: {Saldo} zł, oprocentowanie: {Procent}%");
        }
    }
}
