using Product.Data.Entity;
using GraphQL.Types;

namespace Product.GraphQL.Types
{
    public class ProductReviewType : ObjectGraphType<ProductReview>
    {
        public ProductReviewType()
        {
            Field(t => t.Id);
            Field(t => t.Title);
            Field(t => t.Review);
            Field(t => t.ProductId);
        }
    }
}