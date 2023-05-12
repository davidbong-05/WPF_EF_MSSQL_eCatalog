using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WPF_EF_MSSQL_eCatalog.Contexts;
using WPF_EF_MSSQL_eCatalog.Models;

namespace WPF_EF_MSSQL_eCatalog.Services
{
    public class ProductService
    {
        private readonly ApplicationDbContext _Context;

        public ProductService(ApplicationDbContext Context)
        {
            _Context = Context;
        }

        public async Task<List<Product>> GetProductsAsync(int page, int pageSize)
        {
            return await _Context.Products
                .Include(p => p.ProductCategories)
                    .ThenInclude(pc => pc.Category)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        public async Task<int> GetTotalProductCountAsync()
        {
            return await _Context.Products.CountAsync();
        }
    }
}