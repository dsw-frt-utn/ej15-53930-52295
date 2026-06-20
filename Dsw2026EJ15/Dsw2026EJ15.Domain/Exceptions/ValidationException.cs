using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026EJ15.Domain.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(String message) : base(message) { }
    }
}
