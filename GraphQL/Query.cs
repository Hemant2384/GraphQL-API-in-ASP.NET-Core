using Product.GraphQL.Types;
using Product.Repository;
using GraphQL;
using GraphQL.Types;
using Product.Types;
using Product.Interfaces;

namespace Product.GraphQL
{
    public class Query : ObjectGraphType
    {
        public Query(IProductRepository productRepository, IProductReviewRepository reviewRepository)
        {
            //specify fields and resolve
            Field<ListGraphType<ProductType>>( //type of field
                "products", //name and description shows up in schema docs and name used for querying
                resolve: context => productRepository.GetAll() // resolver tells where to get data from
            );

            //getting prod details by id
            Field<ProductType>(
                "product",
                //creating an non null argument for "product" query with product id as the parameter
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("id"); //getting the id from parameter in query using context object and get argument method
                    return productRepository.GetOne(id);
                }
            );

            Field<ListGraphType<ProductReviewType>>(
                "reviews",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "productId" }),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("productId");
                    return reviewRepository.GetForProduct(id);
                });
        }
    }
}