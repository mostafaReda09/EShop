using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Exceptions
{
    public class DomainException(string message) :Exception($"Domain Exception:{message}")
    {
    }
}
