using Product.GraphQL.Types;
using Product.Repository;
using GraphQL;
using GraphQL.Types;
using Product.Data.Entity;
using Product.Interfaces;


namespace Product.GraphQL
{
    public class Mutation : ObjectGraphType<ProductReviewInputType>
    {
        public Mutation(IProductReviewRepository reviewRepository)
        {
            FieldAsync<ProductReviewType,ProductReview>(
                "createReview",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<ProductReviewInputType>> { Name = "myreview" }),
                resolve: async context =>
                {
                    var review = context.GetArgument<ProductReview>("myreview");
                    //var id = context.GetArgument<int>("productId");
                    await reviewRepository.AddReview(review);
                    return review;
                });
        }
    }
}
