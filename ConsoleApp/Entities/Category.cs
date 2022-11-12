#nullable disable
using System;
using System.Collections.Generic;

namespace DataAccessLayer
{
    /// High-level product categorization.
    public partial class Category
    {
        public Category()
        {
            InverseParentCategory = new HashSet<Category>();
            Products = new HashSet<Product>();
        }

        /// Primary key for ProductCategory records.
        public int CategoryId { get; set; }
        /// Product category identification number of immediate ancestor category. Foreign key to ProductCategory.ProductCategoryID.
        public int? ParentCategoryId { get; set; }
        /// Category description.
        public string Name { get; set; }
        /// Date and time the record was last updated.
        public DateTime ModifiedDate { get; set; }

        public virtual Category ParentCategory { get; set; }
        public virtual ICollection<Category> InverseParentCategory { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}