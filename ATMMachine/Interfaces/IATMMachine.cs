using System;
using System.Collections.Generic;
using System.Text;

namespace ATM
{
    public interface IATMMachine
    {
        string Manufacturer { get; }

        string SerialNumber { get; }

        void InsertCard();

        decimal GetCardBalance();

        Money WithdrawMoney(int amount);

        void ReturnCard();

        void LoadMoney(Money money);

        IEnumerable<Fee> RetrieveChargedFees();
    }
}
