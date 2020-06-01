using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public class BankAccount : IBankAccount
    {
        public string CardNumber { get; }

        public decimal Balance { get; set; }

        public string AccountNumber { get; }

        public List<Transaction> Transactions { get; set; }

        public BankAccount()
        {
            Transactions = new List<Transaction>();
        }
    }
}
