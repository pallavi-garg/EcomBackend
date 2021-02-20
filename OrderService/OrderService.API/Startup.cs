using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using OrderService.DataAccess.SQL;
using OrderService.DataAccess.SQL.Interfaces;
using OrderService.DataAccess;
using OrderService.BusinessLogic.Interface;
using OrderService.BusinessLogic;
using System;
using System.Linq;
using OrderService.DataAccess.WebClient;
using OrderService.Shared.Model;

namespace OrderService.API
{
    public class Startup
    {
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

            services.AddControllers();
            services.AddDbContextPool<DBContext>(options =>
            {
                options.UseMySql(Configuration["ConnectionString:OrderDB"], new MySqlServerVersion(new System.Version(8, 0, 21)));
            });
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IOrderProvider, OrderProvider>();
            //services.AddAuthentication();
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

            app.UseRouting();

            UseCors(app);
            app.UseAuthorization();
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
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
    }
}
