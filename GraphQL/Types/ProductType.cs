using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using Product.Data.Entity;
using Product.Repository;
using GraphQL.DataLoader;
using GraphQL.Types;
using Product.GraphQL.Types;

namespace Product.Types
{
    public class ProductType : ObjectGraphType<Products>
    {
        public ProductType(ProductReviewRepository reviewRepository, IDataLoaderContextAccessor dataLoaderAccessor)
        {
            Field(t => t.Id);
            Field(t => t.Name);
            Field(t => t.Description);
            Field(t => t.IntroducedAt).Description("When the product was first introduced in the catalog");
            Field(t => t.PhotoFileName).Description("The file name of the photo so the client can render it");
            Field(t => t.Price);
            Field(t => t.Rating).Description("The (max 5) star customer rating");
            Field(t => t.Stock);
            Field<ListGraphType<ProductReviewType>, IEnumerable<ProductReview>>()
                .Name("reviews")
                .ResolveAsync(context =>
                {
                    //Example of DataLoader to batch request for loading a collection of items by key
                    //This is used when a key may be associated with more than one item. LoadAsync() is called by the resolver for each user.
                    var user = (ClaimsPrincipal)context.UserContext["User"];
                    var loader =
                        dataLoaderAccessor.Context.GetOrAddCollectionBatchLoader<int, ProductReview>(
                            "GetReviewsByProductId", reviewRepository.GetForProducts);
                    return loader.LoadAsync(context.Source.Id);//load data for the given key
                });
        }
    }
}