using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMMachine.CustomExceptions
{
    public class InsufficientAmountOnAccountForWithdrawException : Exception
    {
        public InsufficientAmountOnAccountForWithdrawException(string message): base(message) { }
    }
}
