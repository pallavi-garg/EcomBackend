namespace AzureCosmos.ReadService
{
    using Microsoft.Azure.Cosmos;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Services.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    public class CosmosReadService : IReadService
    {
        #region Private Members

        private readonly Container container;
        private readonly ILogger<CosmosReadService> logger;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="connectionSring"></param>
        /// <param name="cosmosDatabaseId"></param>
        /// <param name="containerName"></param>
        /// <param name="logger"></param>
        public CosmosReadService(IConfiguration configuration, string connectionSring, string cosmosDatabaseId, string containerName,
            ILogger<CosmosReadService> logger)
        {
            CosmosClient cosmosClient = new CosmosClient(configuration[connectionSring]);
            container = cosmosClient.GetContainer(configuration[cosmosDatabaseId], configuration[containerName]);
            this.logger = logger;
        }

        #endregion

        #region IReadService Members

        /// <summary>
        /// Get an item by Id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetItemById<T>(string id)
        {
            using (var feedIterator = container.GetItemQueryIterator<T>($"SELECT * FROM c WHERE c.id={id}"))
            {
                if (feedIterator.HasMoreResults)
                {
                    var response = feedIterator.ReadNextAsync().Result;
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        logger.LogError($"Unable to get item by Id. Api returned {response.StatusCode}");
                        throw new InvalidOperationException("Unable to get item by Id.");
                    }
                    return response.Resource.FirstOrDefault();
                }
            }
            return default(T);
        }

        /// <summary>
        /// Get an item by query
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<T> GetItemByQuery<T>(string query)
        {
            List<T> documents = new List<T>();

            using (var feedIterator = container.GetItemQueryIterator<T>(query))
            {
                if (feedIterator.HasMoreResults)
                {
                    var response = feedIterator.ReadNextAsync().Result;
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        logger.LogError($"Unable to get item by query. Api returned {response.StatusCode}");
                        throw new InvalidOperationException("Unable to get item by query.");
                    }
                    documents.AddRange(response.Resource);
                }
            }
            return documents;
        }

        /// <summary>
        /// Get all the items
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> GetAllItems<T>()
        {
            List<T> documents = new List<T>();

            using (var feedIterator = container.GetItemQueryIterator<T>("SELECT * FROM c"))
            {
                if (feedIterator.HasMoreResults)
                {
                    var response = feedIterator.ReadNextAsync().Result;
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        logger.LogError($"Unable to get all items. Api returned {response.StatusCode}");
                        throw new InvalidOperationException("Unable to get all items.");
                    }
                    documents.AddRange(response.Resource);
                }
            }
            return documents;
        }

        #endregion

    }
}
