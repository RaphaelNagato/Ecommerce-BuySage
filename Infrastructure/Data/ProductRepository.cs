using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _ctx;
        public ProductRepository(StoreContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _ctx.Products.FindAsync(id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await _ctx.Products.ToListAsync();
        }
    }
}