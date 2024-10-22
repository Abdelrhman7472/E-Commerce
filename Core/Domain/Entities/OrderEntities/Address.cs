using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.OrderEntities
{
    public class Address //(Zy fekret Complex Attributes in SQL)
    {

        public Address() // lazm 3shan el EFCore te3ml migration yekon fe parameterless Ctor
        {
             
        }
        public Address(string firstName, string lastName, string street, string city, string country)
        {
            FirstName = firstName;
            LastName = lastName;
            Street = street;
            City = city;
            Country = country;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
