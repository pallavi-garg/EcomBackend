using Autofac;
using AzureCosmos.ReadService;
using AzureCosmos.WriteService;
using Microsoft.Azure.ServiceBus.Core;
using ProductService.AzureBus;
using ProductService.BusinessLogic;
using ProductService.DataAccess;
using Services.Contracts;

namespace IoC
{
    public class ProductContainerModule : Module
    {
        /// <summary>
        /// Register dependencies
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {

            // DI for Product
            builder.RegisterType<ProductDetailsProvider>().As<IProductDetailsProvider>()
                .WithParameter(
                    (pi, ctx) => pi.ParameterType == typeof(IBaseDataAccessBridge),
                    (pi, ctx) => ctx.ResolveNamed<IBaseDataAccessBridge>("ProductDetailDataAccess"));
            builder.RegisterType<DataAccessBridge>().Keyed<IBaseDataAccessBridge>("ProductDetailDataAccess")
                .WithParameter(
                    (pi, ctx) => pi.ParameterType == typeof(IReadService),
                    (pi, ctx) => ctx.ResolveNamed<IReadService>("ProductDetailReadService"))
                .WithParameter(
                    (pi, ctx) => pi.ParameterType == typeof(IWriteService),
                    (pi, ctx) => ctx.ResolveNamed<IWriteService>("ProductDetailWriteService"));
            builder.RegisterType<CosmosReadService>().Keyed<IReadService>("ProductDetailReadService")
                .WithParameter(new NamedParameter("connectionStringKey", "CosmosEndpointConnectionString"))
                .WithParameter(new NamedParameter("cosmosDatabaseIdKey", "CosmosDatabaseId"))
                .WithParameter(new NamedParameter("containerNameKey", "ProductDetailsCosmosCollectionId"));
            builder.RegisterType<CosmosWriteService>().Keyed<IWriteService>("ProductDetailWriteService")
                .WithParameter(new NamedParameter("connectionStringKey", "CosmosEndpointConnectionString"))
                .WithParameter(new NamedParameter("cosmosDatabaseIdKey", "CosmosDatabaseId"))
                .WithParameter(new NamedParameter("containerNameKey", "ProductDetailsCosmosCollectionId"));


            // DI for Category
            builder.RegisterType<CategoryProvider>().As<ICategoryProvider>()
                .WithParameter(
                    (pi, ctx) => pi.ParameterType == typeof(IBaseDataAccessBridge),
                    (pi, ctx) => ctx.ResolveNamed<IBaseDataAccessBridge>("CatalogDataAccess"));
            builder.RegisterType<DataAccessBridge>().Keyed<IBaseDataAccessBridge>("CatalogDataAccess")
                .WithParameter(
                    (pi, ctx) => pi.ParameterType == typeof(IReadService),
                    (pi, ctx) => ctx.ResolveNamed<IReadService>("CatalogReadService"))
                .WithParameter(
                    (pi, ctx) => pi.ParameterType == typeof(IWriteService),
                    (pi, ctx) => ctx.ResolveNamed<IWriteService>("CatalogWriteService"));
            builder.RegisterType<CosmosReadService>().Keyed<IReadService>("CatalogReadService")
                .WithParameter(new NamedParameter("connectionStringKey", "CosmosEndpointConnectionString"))
                .WithParameter(new NamedParameter("cosmosDatabaseIdKey", "CosmosDatabaseId"))
                .WithParameter(new NamedParameter("containerNameKey", "CatalogCosmosCollectionId"));
            builder.RegisterType<CosmosWriteService>().Keyed<IWriteService>("CatalogWriteService")
                .WithParameter(new NamedParameter("connectionStringKey", "CosmosEndpointConnectionString"))
                .WithParameter(new NamedParameter("cosmosDatabaseIdKey", "CosmosDatabaseId"))
                .WithParameter(new NamedParameter("containerNameKey", "CatalogCosmosCollectionId"));
            builder.RegisterType<ProductInventoryManager>().As<IProductInventoryManager>();
            builder.RegisterType<ProductService.AzureBus.MessageReceiver>().As<ProductService.AzureBus.IMessageReceiver>().SingleInstance();
        }
    }
}
