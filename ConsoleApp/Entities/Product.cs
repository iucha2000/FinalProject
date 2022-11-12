#nullable disable
using System;
using System.Collections.Generic;

namespace DataAccessLayer
{
    /// Products sold or used in the manfacturing of sold products.
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        /// Primary key for Product records.
        public int ProductId { get; set; }
        /// Name of the product.
        public string Name { get; set; }
        /// Unique product identification number.
        public string ProductNumber { get; set; }
        /// Product color.
        public string Color { get; set; }
        /// Standard cost of the product.
        public decimal StandardCost { get; set; }
        /// Selling price.
        public decimal ListPrice { get; set; }
        /// Product size.
        public string Size { get; set; }
        /// Product weight.
        public decimal? Weight { get; set; }
        /// Product is a member of this product category. Foreign key to ProductCategory.ProductCategoryID. 
        public int? ProductCategoryId { get; set; }
        /// Product is a member of this product model. Foreign key to ProductModel.ProductModelID.
        public int? ProductModelId { get; set; }
        /// Date the product was available for sale.
        public DateTime SellStartDate { get; set; }
        /// Date the product was no longer available for sale.
        public DateTime? SellEndDate { get; set; }
        /// Date the product was discontinued.
        public DateTime? DiscontinuedDate { get; set; }
        /// Small image file name.
        public string PhotoFileName { get; set; }
        /// Date and time the record was last updated.
        public DateTime ModifiedDate { get; set; }

        public virtual Category ProductCategory { get; set; }
        public virtual ProductModel ProductModel { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}