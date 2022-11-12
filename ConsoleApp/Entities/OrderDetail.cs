#nullable disable
using System;
using System.Collections.Generic;

namespace DataAccessLayer
{
    /// Individual products associated with a specific sales order. See SalesOrderHeader.
    public partial class OrderDetail
    {
        /// Primary key. Foreign key to SalesOrderHeader.SalesOrderID.
        public int OrderId { get; set; }
        /// Primary key. One incremental unique number per product sold.
        public int OrderDetailId { get; set; }
        /// Quantity ordered per product.
        public short OrderQty { get; set; }
        /// Product sold to customer. Foreign key to Product.ProductID.
        public int ProductId { get; set; }
        /// Selling price of a single product.
        public decimal UnitPrice { get; set; }
        /// Discount amount.
        public decimal UnitPriceDiscount { get; set; }
        /// Per product subtotal. Computed as UnitPrice * (1 - UnitPriceDiscount) * OrderQty.
        public decimal LineTotal { get; set; }
        /// Date and time the record was last updated.
        public DateTime ModifiedDate { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}