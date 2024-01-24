using Microsoft.EntityFrameworkCore;
using Project.Core;
using Project.Core.Repositories;

namespace Project.Data.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ProjectDbContext context) : base(context)
        {
        }

        public async Task<List<Product>> GetProductsWitCategory()
        {

            return await _context.Products.Include(x => x.Category).ToListAsync();
        }
    }
}
