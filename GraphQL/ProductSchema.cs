using System;
using GraphQL;
using GraphQL.Types;
using GraphQL.Utilities;
using Product.GraphQL;


namespace Product.GraphQL
{
    public class ProductSchema : Schema
    {
        public ProductSchema(Query query, Mutation mutation, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            Query = query;
            Mutation = mutation;
        }
    }
}