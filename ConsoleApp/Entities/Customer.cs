#nullable disable
using System;
using System.Collections.Generic;

namespace DataAccessLayer
{
    /// Customer information.
    public partial class Customer
    {
        public Customer()
        {
            CustomerAddresses = new HashSet<CustomerAddress>();
            Orders = new HashSet<Order>();
        }

        /// Primary key for Customer records.
        public int CustomerId { get; set; }
        /// 0 = The data in FirstName and LastName are stored in western style (first name, last name) order.  1 = Eastern style (last name, first name) order.
        public bool NameStyle { get; set; }
        /// A courtesy title. For example, Mr. or Ms.
        public string Title { get; set; }
        /// First name of the person.
        public string FirstName { get; set; }
        /// Middle name or middle initial of the person.
        public string MiddleName { get; set; }
        /// Last name of the person.
        public string LastName { get; set; }
        /// Surname suffix. For example, Sr. or Jr.
        public string Suffix { get; set; }
        /// The customer&apos;s organization.
        public string CompanyName { get; set; }
        /// The customer&apos;s sales person, an employee of AdventureWorks Cycles.
        public string SalesPerson { get; set; }
        /// E-mail address for the person.
        public string EmailAddress { get; set; }
        /// Phone number associated with the person.
        public string Phone { get; set; }
        /// Password for the e-mail account.
        public string PasswordHash { get; set; }
        /// Random value concatenated with the password string before the password is hashed.
        public string PasswordSalt { get; set; }
        /// Date and time the record was last updated.
        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<CustomerAddress> CustomerAddresses { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}