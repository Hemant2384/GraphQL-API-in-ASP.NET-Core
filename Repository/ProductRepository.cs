using System.Collections.Generic;
using System.Threading.Tasks;
using Product.Data;
using Product.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace Product.Repository
{
    public class ProductRepository
    {
        private readonly ProductContext _dbContext;

        public ProductRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Products>> GetAll()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<Products> GetOne(int id)
        {
            return await _dbContext.Products.SingleOrDefaultAsync(p => p.Id == id);
        }
    }
}