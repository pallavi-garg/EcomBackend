namespace AzureCosmos.ReadService
{
    using Microsoft.Azure.Cosmos;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using ProductService.Shared;
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
        /// <param name="connectionStringKey"></param>
        /// <param name="cosmosDatabaseIdKey"></param>
        /// <param name="containerNameKey"></param>
        /// <param name="logger"></param>
        public CosmosReadService(IConfiguration configuration, string connectionStringKey, string cosmosDatabaseIdKey, string containerNameKey,
            ILogger<CosmosReadService> logger)
        {
            CosmosClient cosmosClient = new CosmosClient(configuration[connectionStringKey]);
            container = cosmosClient.GetContainer(configuration[cosmosDatabaseIdKey], configuration[containerNameKey]);
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
        /// <param name="continuationToken"></param>
        /// <returns></returns>
        public SearchResult<T> GetItemByQuery<T>(string query, string continuationToken)
        {
            SearchResult<T> searchResult = new SearchResult<T>();
            List<T> documents = new List<T>();

            using (var feedIterator = container.GetItemQueryIterator<T>(query, continuationToken, new QueryRequestOptions { MaxItemCount = 15 }))
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
                    searchResult.ContinuationToken = response.ContinuationToken;
                }
                searchResult.Data = documents;
            }
            return searchResult;
        }

        /// <summary>
        /// Get item count by query
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public int GetItemCountByQuery(string query)
        {
            int count = 0;
            using (var feedIterator = container.GetItemQueryIterator<IDictionary<string, int>>(query))
            {
                if (feedIterator.HasMoreResults)
                {
                    var response = feedIterator.ReadNextAsync().Result;
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        logger.LogError($"Unable to get item by query. Api returned {response.StatusCode}");
                        throw new InvalidOperationException("Unable to get item by query.");
                    }
                    count = response.First().First(x => x.Key == "$1").Value;
                }
            }
            return count;
        }



        /// <summary>
        /// Get all the items
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public SearchResult<T> GetAllItems<T>(string continuationToken)
        {
            SearchResult<T> searchResult = new SearchResult<T>();
            List<T> documents = new List<T>();

            using (var feedIterator = container.GetItemQueryIterator<T>("SELECT * FROM c", continuationToken, new QueryRequestOptions { MaxItemCount = 15 }))
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
                    searchResult.ContinuationToken = response.ContinuationToken;
                }
                searchResult.Data = documents;
            }
            return searchResult;
        }

        #endregion

    }
}
