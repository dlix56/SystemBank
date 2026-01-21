using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BankS.Bank
{
    [Serializable]
    public class KontoOsobiste : Konto
    {
        public KontoOsobiste(string nrkonta, decimal saldoStart) : base(nrkonta, saldoStart)
        {
        }

        public override void Wyswietl()
        {
            Console.WriteLine($"Konto osobiste: {NrKonta}, saldo: {Saldo} zł");
        }
    }
}
