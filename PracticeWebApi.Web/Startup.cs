using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PracticeWebApi.CommonClasses.Orders;
using PracticeWebApi.CommonClasses.Products;
using PracticeWebApi.CommonClasses.Users;
using PracticeWebApi.Data;
using PracticeWebApi.Data.Orders;
using PracticeWebApi.Data.Products;
using PracticeWebApi.Data.Users;
using PracticeWebApi.Services;
using PracticeWebApi.Services.Orders;
using PracticeWebApi.Services.Products;
using PracticeWebApi.Services.Users;

namespace PracticeWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // config
            var databaseConfiguration = new DatabaseConfiguration();
            Configuration.GetSection("DatabaseConfiguration").Bind(databaseConfiguration);
            services.AddSingleton(databaseConfiguration);

            // repositories - adding flag for using InMemory or not
            switch (databaseConfiguration.RepositoryType)
            {
                case "InMemory": 
                    services.AddSingleton<IUserRepository, FakeUserRepository>();
                    services.AddSingleton<IProductGroupRepository, FakeProductGroupRepository>();
                    services.AddSingleton<IProductRepository, FakeProductRepository>();
                    services.AddSingleton<IOrderRepository, FakeOrderRepository>();
                    break;
                default:
                    services.AddSingleton<IUserRepository, UserRepository>();
                    services.AddSingleton<IProductGroupRepository, ProductGroupRepository>();
                    services.AddSingleton<IProductRepository, ProductRepository>();
                    services.AddSingleton<IOrderRepository, OrderRepository>();
                    break;
            }
            

            // users
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IMapper<User, UserDataEntity>, UserMapper>();

            // product groups
            services.AddSingleton<IProductGroupService, ProductGroupService>();
            services.AddSingleton<IMapper<ProductGroup, ProductGroupDataEntity>, ProductGroupMapper>();

            // products
            services.AddSingleton<IProductService, ProductService>();
            services.AddSingleton<IMapper<Product, ProductDataEntity>, ProductMapper>();

            // orders
            services.AddSingleton<IOrderService, OrderService>();
            services.AddSingleton<IMapper<Order, OrderDataEntity>, OrderMapper>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
