using Product.Data.Entity;
using GraphQL.Types;

namespace Product.GraphQL.Types
{
    public class ProductReviewInputType : InputObjectGraphType<ProductReview>
    {
        public ProductReviewInputType()
        {
            Name = "reviewInput";
            Field(r => r.Title).Description("Review Title");
            Field(r => r.Review).Description("Review description");
            Field(r => r.ProductId).Description("Product Id");
        }
    }
}
