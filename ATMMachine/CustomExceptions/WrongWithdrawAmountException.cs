using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMMachine.CustomExceptions
{
    public class WrongWithdrawAmountException : Exception
    {
        public WrongWithdrawAmountException(){}

        public WrongWithdrawAmountException(string message) : base(message) { }
    }
}
