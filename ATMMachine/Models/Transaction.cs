using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public class Transaction
    {
        public Guid TransactionId { get; set; }

        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        public string AccountNumber { get; set; }

        public string CardNumber { get; set; }


        public string ATMSerialNumber { get; set; }
    }
}
