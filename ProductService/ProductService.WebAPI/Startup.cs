using AzureCosmos.ReadService;
using AzureCosmos.WriteService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProductService.AzureBus;
using ProductService.BusinessLogic;
using ProductService.DataAccess;
using Services.Contracts;

namespace ProductService.WebApi
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
            services.AddControllers();
            // DI injection
            services.AddSingleton<IProductDetailsProvider, ProductDetailsProviders>();
            services.AddSingleton<IBaseDataAccessBridge, DataAccessBridge>();
            services.AddSingleton<IProductInventoryManager, ProductInventoryManager>();
            
            services.AddTransient<IReadService, CosmosReadService>(provider => new CosmosReadService(provider.GetService<IConfiguration>(),
                "CosmosEndpointConnectionString", "CosmosDatabaseId", "ProductDetailsCosmosCollectionId", provider.GetService<ILogger<CosmosReadService>>()));
            services.AddTransient<IWriteService, CosmosWriteService>(provider => new CosmosWriteService(provider.GetService<IConfiguration>(),
                "CosmosEndpointConnectionString", "CosmosDatabaseId", "ProductDetailsCosmosCollectionId", provider.GetService<ILogger<CosmosWriteService>>()));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
