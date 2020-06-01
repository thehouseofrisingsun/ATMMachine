using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMMachine.CustomExceptions
{
    public class WrongNoteLoadedException : Exception
    {
        public WrongNoteLoadedException() { }
        public WrongNoteLoadedException(string message) : base(message) { }
    }
}
