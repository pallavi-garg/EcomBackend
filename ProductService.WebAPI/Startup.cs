using AzureCosmos.ReadService;
using AzureCosmos.WriteService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ProductService.BusinessLogic;
using ProductService.DataAccess;
using Services.Contracts;

namespace ProductService.WebApi
{
    public class Startup
    {
        #region Private Members

        private const string enableSwaggerKey = "EnableSwagger";
        private const string swaggerVersion = "v1";
        private const string swaggerTitle = "Product Service API";
        private const string swaggerEndpointName = "Product Service API V1";
        private const string swaggerEndpointUrl = "swagger/v1/swagger.json";
        
        #endregion

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container. 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            RegisterSwagger(services);
            services.AddControllers();
            // DI injection
            services.AddSingleton<IProductDetailsProvider, ProductDetailsProviders>();
            services.AddSingleton<IBaseDataAccessBridge, DataAccessBridge>();
            
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
            UseSwagger(app);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        #region Private Methods

        /// <summary>
        /// Register swagger
        /// </summary>
        /// <param name="services"></param>
        private void RegisterSwagger(IServiceCollection services)
        {
            bool.TryParse(Configuration[enableSwaggerKey], out bool enableSwagger);
            if (enableSwagger)
            {
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc(swaggerVersion, new OpenApiInfo { Title = swaggerTitle, Version = swaggerVersion });

                });
            }
        }

        /// <summary>
        /// Add Swagger configuration in middleware.
        /// </summary>
        /// <param name="app"></param>
        private void UseSwagger(IApplicationBuilder app)
        {
            bool.TryParse(Configuration[enableSwaggerKey], out bool enableSwagger);
            if (enableSwagger)
            {
                // Enable middleware to serve generated Swagger as a JSON endpoint.
                app.UseSwagger();

                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint($"/{swaggerEndpointUrl}", swaggerEndpointName);
                    c.RoutePrefix = string.Empty;
                });
            }
        }

        #endregion
    }
}
