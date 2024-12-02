using MediatR.NotificationPublishers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.ValueObjects
{
    public record Payement
    {
        public string? CardName { get;}
        public string CardNumber { get;} = default!;
        public string Expiration { get;} = default!;
        public string CVV { get;} = default!;
        public int PayementMethod { get;} = default!;
    }
}
