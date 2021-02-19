using CartService.BusinessLogic;
using CartService.BusinessLogic.Interface;
using CartService.DataAccess.SQL;
using CartService.DataAccess.SQL.Interfaces;
using CartService.DataAccess.WebClient;
using CartService.Shared.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Linq;

namespace CartService
{
    public class Startup
    {
        private const string enableSwaggerKey = "EnableSwagger";
        private const string swaggerVersion = "v1";
        private const string swaggerTitle = "Cart Service API";
        private const string swaggerEndpointName = "Cart Service API V1";
        private const string swaggerEndpointUrl = "swagger/v1/swagger.json";
        private const string AllowedOriginsKey = "AllowedOrigins";
        private string[] allowedOrigins = null;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            RegisterCors(services);
            RegisterSwagger(services);
            services.AddHttpClient();
            services.AddControllers();
            services.AddDbContext<DBContext>();
            services.AddScoped<ICartInfoProvider, CartInfoProvider>();
            services.AddSingleton<HttpCalls>();
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            UseSwagger(app);
            UseCors(app);

            app.UseRouting();

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


        /// <summary>
        /// Register CORS with middleware.
        /// </summary>
        /// <param name="services"></param>
        private void RegisterCors(IServiceCollection services)
        {
            string allowedHostsConfigValue = Configuration[AllowedOriginsKey];
            allowedOrigins = (string.IsNullOrWhiteSpace(allowedHostsConfigValue)) ? null :
                                allowedHostsConfigValue.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(origin => origin.Trim()).ToArray();

            // Register CORS only if any hosts are provided
            if (allowedOrigins != null && allowedOrigins.Any())
            {
                services.AddCors();
            }
        }

        /// <summary>
        /// Add origins which are allowed to call this service.
        /// </summary>
        /// <param name="app"></param>
        private void UseCors(IApplicationBuilder app)
        {
            // Enable CORS for specified hosts only
            if (allowedOrigins != null && allowedOrigins.Any())
            {
                app.UseCors(options => options.WithOrigins(allowedOrigins).WithMethods("GET", "POST", "PUT").WithHeaders("*"));
            }
        }

        #endregion
    }
}
