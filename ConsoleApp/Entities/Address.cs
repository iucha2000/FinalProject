#nullable disable
using System;
using System.Collections.Generic;

namespace DataAccessLayer
{
    /// Street address information for customers.
    public partial class Address
    {
        public Address()
        {
            CustomerAddresses = new HashSet<CustomerAddress>();
            OrderBillToAddresses = new HashSet<Order>();
            OrderShipToAddresses = new HashSet<Order>();
        }

        /// Primary key for Address records.
        public int AddressId { get; set; }
        /// First street address line.
        public string AddressLine1 { get; set; }
        /// Second street address line.
        public string AddressLine2 { get; set; }
        /// Name of the city.
        public string City { get; set; }
        /// Name of state or province.
        public string StateProvince { get; set; }
        public string CountryRegion { get; set; }
        /// Postal code for the street address.
        public string PostalCode { get; set; }
        /// Date and time the record was last updated.
        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<CustomerAddress> CustomerAddresses { get; set; }
        public virtual ICollection<Order> OrderBillToAddresses { get; set; }
        public virtual ICollection<Order> OrderShipToAddresses { get; set; }
    }
}