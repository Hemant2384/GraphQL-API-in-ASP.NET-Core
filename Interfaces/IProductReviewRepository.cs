using Product.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Interfaces
{
    public interface IProductReviewRepository
    {
        Task<ProductReview> AddReview(ProductReview review);
        Task<IEnumerable<ProductReview>> GetForProduct(int productId);
        Task<ILookup<int, ProductReview>> GetForProducts(IEnumerable<int> productIds);
    }
}