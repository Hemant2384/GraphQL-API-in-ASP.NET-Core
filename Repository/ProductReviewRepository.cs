using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Product.Data;
using Product.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace Product.Repository
{
    public class ProductReviewRepository : IProductReviewRepository
    {
        private readonly ProductContext _dbContext;

        public ProductReviewRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ProductReview>> GetForProduct(int productId)
        {
            return await _dbContext.ProductReviews.Where(pr => pr.ProductId == productId).ToListAsync();
        }

        //ILookip can have multiple values against a single key, for every product we can have multiple reviews
        public async Task<ILookup<int, ProductReview>> GetForProducts(IEnumerable<int> productIds)
        {
            var reviews = await _dbContext.ProductReviews.Where(pr => productIds.Contains(pr.ProductId)).ToListAsync();
            return reviews.ToLookup(r => r.ProductId);
        }

        public async Task<ProductReview> AddReview(ProductReview review)
        {
            _dbContext.ProductReviews.Add(review);
            await _dbContext.SaveChangesAsync();
            return review;
        }
    }
}