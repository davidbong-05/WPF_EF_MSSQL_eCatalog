using System.Collections.Generic;
namespace WPF_EF_MSSQL_eCatalog.Models
{
    public class Product : Base
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public virtual ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();

        public Product() { }
        public Product(int productId, string name, string imgUrl, string desc, double price)
        {
            ProductId = productId;
            Name = name;
            ImageUrl = imgUrl;
            Description = desc;
            Price = price;
        }
    }
}
