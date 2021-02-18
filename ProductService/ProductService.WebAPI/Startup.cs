using AzureCosmos.ReadService;
using AzureCosmos.WriteService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using ProductService.AzureBus;
using ProductService.BusinessLogic;
using ProductService.DataAccess;
using Services.Contracts;
using System.IdentityModel.Tokens.Jwt;

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
            // to make the incoming token claims value default and not what microsoft uses
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();


            IdentityModelEventSource.ShowPII = true;
            services.AddControllers();
            // DI injection
            services.AddSingleton<IProductDetailsProvider, ProductDetailsProviders>();
            services.AddSingleton<IBaseDataAccessBridge, DataAccessBridge>();
            services.AddSingleton<IProductInventoryManager, ProductInventoryManager>();
            
            services.AddTransient<IReadService, CosmosReadService>(provider => new CosmosReadService(provider.GetService<IConfiguration>(),
                "CosmosEndpointConnectionString", "CosmosDatabaseId", "ProductDetailsCosmosCollectionId", provider.GetService<ILogger<CosmosReadService>>()));
            services.AddTransient<IWriteService, CosmosWriteService>(provider => new CosmosWriteService(provider.GetService<IConfiguration>(),
                "CosmosEndpointConnectionString", "CosmosDatabaseId", "ProductDetailsCosmosCollectionId", provider.GetService<ILogger<CosmosWriteService>>()));

            services.AddSingleton<IMessageReceiver, MessageReceiver>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = "https://topnotchprod.b2clogin.com/topnotchprod.onmicrosoft.com/B2C_1_susi_social_idp/v2.0/";
                    options.Audience = "8296b285-7ad8-46ce-89de-4b0562d1028d";
                });
            services.AddAuthorization(
                options =>
                {
                    options.AddPolicy(
                        "Admin",
                        policy => policy.RequireClaim("extension_Role", "212342"));
                });

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

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var messageReceiver = app.ApplicationServices.GetService<IMessageReceiver>();
            //TODO: Uncomment when service bus settings are added
            //messageReceiver.StartReceivingOrdersMadeRequest(1);
        }
    }
}
