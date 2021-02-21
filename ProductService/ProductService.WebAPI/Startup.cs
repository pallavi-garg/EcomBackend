using Autofac;
using IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.ServiceBus.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;

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
        private const string AllowedOriginsKey = "AllowedOrigins";
        private string[] allowedOrigins = null;

        #endregion

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// This is the default if you don't have an environment specific method.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new ProductContainerModule());
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container. 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            RegisterSwagger(services);
            RegisterCors(services);

            services.AddControllers();
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
            services.AddSingleton<IMessageReceiver, MessageReceiver>();
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
            UseCors(app);


            app.UseAuthentication();

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
