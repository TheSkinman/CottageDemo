using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CottageApp.Models
{
    public class Cottage
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public double Bathrooms { get; set; }

        public double Bedrooms { get; set; }

        public DateTime BuiltDate { get; set; }

        public string Summary { get; set; }

        public int MaxGuest { get; set; }

        public double SquareFootage { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }
    }
}
