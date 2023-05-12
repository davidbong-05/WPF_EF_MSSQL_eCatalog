namespace WPF_EF_MSSQL_eCatalog.Models
{
    public class ProductCategory : Base
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; } = new Product();
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; } = new Category();
        public ProductCategory() { }
        public ProductCategory(Product product, Category category)
        {
            Product = product;
            Category = category;
        }
    }
}
