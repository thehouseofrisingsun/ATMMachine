using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public interface IBankAccount
    {
        string CardNumber { get; }

        string AccountNumber { get; }

        decimal Balance { get; set; }

        List<Transaction> Transactions { get; set; }
    }
}
