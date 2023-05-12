using System;
using System.Collections.Generic;

namespace WPF_EF_MSSQL_eCatalog.Models
{
    public class Category : Base
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
        public Category() { }

        public Category(int categoryId, string name)
        {
            CategoryId = categoryId;
            Name = name;
        }

        public override bool Equals(object? obj)
        {
            return obj is Category product &&
                   Name == product.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }
    }
}
