using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Exceptions
{
    public class ValidationException : ArgumentException
    {
        public ValidationException(string message) :
            base(message) {; }
    }
}
