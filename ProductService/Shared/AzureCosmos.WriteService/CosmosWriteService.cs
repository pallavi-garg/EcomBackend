namespace AzureCosmos.WriteService
{
    using Microsoft.Azure.Cosmos;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Services.Contracts;
    using System;
    using System.Net;

    public class CosmosWriteService : IWriteService
    {
        #region Private Members

        private readonly ILogger<CosmosWriteService> logger;
        private readonly Container container;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="connectionStringKey"></param>
        /// <param name="cosmosDatabaseIdKey"></param>
        /// <param name="containerNameKey"></param>
        /// <param name="logger"></param>
        public CosmosWriteService(IConfiguration configuration, string connectionStringKey, string cosmosDatabaseIdKey,
            string containerNameKey, ILogger<CosmosWriteService> logger)
        {
            CosmosClient cosmosClient = new CosmosClient(configuration[connectionStringKey]);
            container = cosmosClient.GetContainer(configuration[cosmosDatabaseIdKey], configuration[containerNameKey]);
            this.logger = logger;
        }

        #endregion

        #region IWriteService Members

        /// <summary>
        /// Update an item
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public T UpdateItem<T>(string id, T item)
        {
            var response = container.UpsertItemAsync(item).Result;
            if (response.StatusCode != HttpStatusCode.OK)
            {
                logger.LogError($"Unable to update item. Api returned {response.StatusCode}");
                throw new InvalidOperationException("Unable to update item.");
            }
            return response.Resource;
        }

        /// <summary>
        /// Add new item
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>

        public T AddItem<T>(T item)
        {
            // add try catch in all and give support of retry
            var response = container.CreateItemAsync(item).Result;
            if (response.StatusCode != HttpStatusCode.Created)
            {
                logger.LogError($"Unable to create item. Api returned {response.StatusCode}");
                throw new InvalidOperationException("Unable to create item.");
            }
            return response.Resource;
        }

        /// <summary>
        /// Delete an item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteItem(string id)
        {
            var response = container.DeleteItemAsync<bool>(id, new PartitionKey("/id"), null).Result;
            if (response.StatusCode != HttpStatusCode.OK)
            {
                logger.LogError($"Unable to delete item. Api returned {response.StatusCode}");
                throw new InvalidOperationException("Unable to delete item.");
            }
            return response.Resource;
        }


        #endregion

    }
}
