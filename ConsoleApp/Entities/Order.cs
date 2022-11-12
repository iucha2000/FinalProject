#nullable disable
using System;
using System.Collections.Generic;

namespace DataAccessLayer
{
    /// General sales order information.
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        /// Primary key.
        public int OrderId { get; set; }
        /// Incremental number to track changes to the sales order over time.
        public byte RevisionNumber { get; set; }
        /// Dates the sales order was created.
        public DateTime OrderDate { get; set; }
        /// Date the order was shipped to the customer.
        public DateTime? ShipDate { get; set; }
        /// Order current status. 1 = In process; 2 = Approved; 3 = Backordered; 4 = Rejected; 5 = Shipped; 6 = Cancelled
        public byte Status { get; set; }
        public string OrderNumber { get; set; }
        /// 0 = Order placed by sales person. 1 = Order placed online by customer.
        public bool? OnlineOrderFlag { get; set; }
        /// Customer purchase order number reference. 
        public string PurchaseOrderNumber { get; set; }
        /// Financial accounting number reference.
        public string AccountNumber { get; set; }
        /// Customer identification number. Foreign key to Customer.CustomerID.
        public int CustomerId { get; set; }
        /// The ID of the location to send goods.  Foreign key to the Address table.
        public int? ShipToAddressId { get; set; }
        /// The ID of the location to send invoices.  Foreign key to the Address table.
        public int? BillToAddressId { get; set; }
        /// Shipping method. Foreign key to ShipMethod.ShipMethodID.
        public string ShipMethod { get; set; }
        /// Approval code provided by the credit card company.
        public string CreditCardApprovalCode { get; set; }
        /// Sales subtotal. Computed as SUM(SalesOrderDetail.LineTotal)for the appropriate SalesOrderID.
        public decimal SubTotal { get; set; }
        /// Tax amount.
        public decimal TaxAmt { get; set; }
        /// Shipping cost.
        public decimal Freight { get; set; }
        /// Total due from customer. Computed as Subtotal + TaxAmt + Freight.
        public decimal TotalDue { get; set; }
        /// Sales representative comments.
        public string Comment { get; set; }
        /// Date and time the record was last updated.
        public DateTime ModifiedDate { get; set; }

        public virtual Address BillToAddress { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Address ShipToAddress { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}