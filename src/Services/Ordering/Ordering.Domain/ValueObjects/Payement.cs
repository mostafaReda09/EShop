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

        private Payement(string cardName,string cardNumber,string expiration,string cvv,int payementMethod)
        {
            CardName = cardName;
            CardNumber = cardNumber;
            Expiration = expiration;
            CVV = cvv;
            PayementMethod = payementMethod;

        }
        public static Payement Of(string cardName, string cardNumber, string expiration, string cvv, int payementMethod)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(cardName);
            ArgumentException.ThrowIfNullOrWhiteSpace(cardNumber);
            ArgumentException.ThrowIfNullOrWhiteSpace(cvv);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(cvv.Length, 3);
            return new Payement(cardName,cardNumber,expiration,cvv,payementMethod);
        }
    }
}
