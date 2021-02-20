using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using ProductService.AzureBus;
using Microsoft.Extensions.Hosting;

namespace ProductService.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            var service = host.Services.GetService(typeof(IMessageReceiver)) as IMessageReceiver;
            service.StartReceivingOrdersMadeRequest(1);
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
