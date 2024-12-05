using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.ValueObjects
{
    public record Address
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string? EmailAddress { get; set; } = default!;
        public string AddressLine { get; set; } = default!;
        public string Country { get; set; } = default!;
        public string State { get; set; } = default!;
        public string ZipCode { get; set; } = default!;
        private Address()
        {
            
        }
        private Address(string firstName,string lastName,string emailAdress,string addressLine,string country,string state,string zipcode)
        {
            FirstName = firstName;
            LastName = lastName;   
            EmailAddress = emailAdress;
            AddressLine = addressLine;
            Country = country;
            State = state;
            ZipCode = zipcode;
        }
        public static Address Of(string firstName, string lastName, string emailAdress, string addressLine, string country, string state, string zipcode) 
        {
            ArgumentException.ThrowIfNullOrEmpty(emailAdress);
            ArgumentException.ThrowIfNullOrEmpty(addressLine);
            return new Address( firstName,  lastName,  emailAdress,  addressLine,  country,  state,  zipcode);
        }
    }
}
