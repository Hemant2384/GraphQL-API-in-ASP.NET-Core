using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using GraphQL.SystemTextJson;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using Microsoft.EntityFrameworkCore;
using Product.Data;
using Product.GraphQL;
using Product.Repository;
using System.Collections.Generic;
using GraphQL.Server.Ui.Altair;

namespace Product
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration config, IWebHostEnvironment env)
        {
            _configuration = config;
            _env = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = _configuration.GetConnectionString("SampleAppDbContext");
            services.AddDbContext<ProductContext>(options =>
                    options.UseSqlServer(connectionString));
            services.AddScoped<ProductRepository>();


            services.AddScoped<ProductReviewRepository>();

            services.AddScoped<ProductSchema>(); //Registering the Schema
            services.AddScoped<Query>();

            services.AddGraphQL()
                .AddSystemTextJson(o => o.PropertyNameCaseInsensitive = true)//Json serialization and deserialization
                .AddGraphTypes(ServiceLifetime.Scoped)//register the various graphql types, scans the assembly and adds all types that implement IGraphType
                .AddUserContextBuilder(httpContext => new Dictionary<string, object> { { "User", httpContext.User } })
                .AddDataLoader()// data loader for dataloader operations
                .AddWebSockets();

            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
                builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseWebSockets();
            app.UseGraphQLWebSockets<ProductSchema>("/graphql");

            //use the HTTP middleware for Schema at default path graphql. Since we have only one end point, controllers
            //can  be replaced with middlewares
            app.UseGraphQL<ProductSchema>();
            app.UseGraphQLAltair(new GraphQLAltairOptions { Path = "/" });
            //app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());

        }
    }
}
