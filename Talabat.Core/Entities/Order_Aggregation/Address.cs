using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Order_Aggregation
{
    public class Address
    {
        public Address()
        {

        }
        // oh nooooo 
        public Address(string firstName, string lastName, string country, string city, string street)
        {
            FristName = firstName;
            LastName = lastName;
            Country = country;
            City = city;
            Street = street;
        }

        public string FristName { get; set; }
        public string LastName { get; set; }

        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }

    }
}
