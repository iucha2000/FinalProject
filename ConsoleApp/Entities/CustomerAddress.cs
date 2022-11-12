#nullable disable
using System;
using System.Collections.Generic;

namespace DataAccessLayer
{
    /// Cross-reference table mapping customers to their address(es).
    public partial class CustomerAddress
    {
        /// Primary key. Foreign key to Customer.CustomerID.
        public int CustomerId { get; set; }
        /// Primary key. Foreign key to Address.AddressID.
        public int AddressId { get; set; }
        /// The kind of Address. One of: Archive, Billing, Home, Main Office, Primary, Shipping
        public string AddressType { get; set; }
        /// Date and time the record was last updated.
        public DateTime ModifiedDate { get; set; }

        public virtual Address Address { get; set; }
        public virtual Customer Customer { get; set; }
    }
}