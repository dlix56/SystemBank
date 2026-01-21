using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankS.Bank.Wyjatki
{
    public class BrakFund : Exception
    {
        public BrakFund() : base("Brak wystarczajacych srodkow")
        { }

        public BrakFund(string message) : base(message)
        {
        } 
    }
}
