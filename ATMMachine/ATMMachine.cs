using ATMMachine.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public class ATMMachine : IATMMachine
    {
        private readonly IBankAccount _bankAccount;

        /// <summary>
        /// ATM machine has use only when it operates with a card(ban account)
        /// </summary>
        /// <param name="account"></param>
        public ATMMachine(IBankAccount account)
        {
            _bankAccount = account;
        }

        public string Manufacturer { get; }

        public string SerialNumber { get; }

        public decimal GetCardBalance()
        {
            return _bankAccount.Balance;
        }

        public void InsertCard() { }

        public void LoadMoney(Money money)
        {
            foreach (var note in money.Notes.Values)
            {
                if (note != 5 || note != 10 || note != 20 || note != 50)
                {
                    throw new WrongNoteLoadedException("Please load 5, 10, 20 or 50");
                }
            }

            _bankAccount.Balance += money.Amount;
        }

        public IEnumerable<Fee> RetrieveChargedFees()
        {
            List<Fee> fees = new List<Fee>();
            var transactions = _bankAccount.Transactions.Where(x => x.ATMSerialNumber == SerialNumber);
            foreach (var t in transactions)
            {
                Fee fee = new Fee
                {
                    CardNumber = _bankAccount.CardNumber,
                    WithdrawalDate = t.Date,
                    WithdrawalFeeAmount = t.Amount
                };
                fees.Add(fee);
            }

            return fees;
        }

        public void ReturnCard() { }

        public Money WithdrawMoney(int amount)
        {
            if (_bankAccount.Balance < amount + (decimal)0.01 * amount)
            {
                throw new InsufficientAmountOnAccountForWithdrawException("There is not enough money on your account to withdraw");
            }

            if (amount % 5 == 0)
            {
                Money money = new Money
                {
                    Amount = amount,
                };

                _bankAccount.Balance -= (decimal)0.01 * amount + amount;

                _bankAccount.Transactions.Add(new Transaction
                {
                    CardNumber = _bankAccount.CardNumber,
                    Amount = amount + (decimal)0.01 * amount,
                    Date = DateTime.Now,
                    TransactionId = new Guid()
                });

                return money;
            }

            throw new WrongWithdrawAmountException();
        }
    }
}
