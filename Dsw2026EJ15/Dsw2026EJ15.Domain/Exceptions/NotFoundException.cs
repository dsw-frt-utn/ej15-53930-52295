using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Dsw2026EJ15.Domain.Exceptions
{
    public class NotFoundException:Exception
    {
      public NotFoundException(string message) : base(message) { }
    }
}
