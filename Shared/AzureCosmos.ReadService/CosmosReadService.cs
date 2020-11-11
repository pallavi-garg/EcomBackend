namespace AzureCosmos.ReadService
{
    using Microsoft.Azure.Documents;
    using Microsoft.Azure.Documents.Client;
    using Microsoft.Azure.Documents.Linq;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using Services.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;

    public class CosmosReadService : IReadService
    {
        #region Private Members

        private readonly string databaseId;
        private string collectionId;
        private readonly ILogger<CosmosReadService> logger;
        private readonly DocumentClient documentClient;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="cosmosEndpointUrl"></param>
        /// <param name="cosmosKey"></param>
        /// <param name="cosmosDatabaseId"></param>
        /// <param name="logger"></param>
        public CosmosReadService(IConfiguration configuration, string cosmosEndpointUrl, string cosmosKey, string cosmosDatabaseId,
            ILogger<CosmosReadService> logger)
        {
            documentClient = new DocumentClient(new Uri(configuration[cosmosEndpointUrl]), configuration[cosmosKey]);
            databaseId = configuration[cosmosDatabaseId];
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
            try
            {
                Document document = documentClient.ReadDocumentAsync(UriFactory.CreateDocumentUri(databaseId, collectionId, id),
                    new RequestOptions { PartitionKey = new PartitionKey(id) }).Result;
                return JsonConvert.DeserializeObject<T>(document.ToString());
            }
            catch (DocumentClientException documentException)
            {
                logger.LogError(documentException, $"Error in fetching document of type {typeof(T)} with id {id}");
                Exception exceptionToThrow = CreateCustomExceptionForDocumentDb(documentException);
                throw exceptionToThrow;
            }
            catch (AggregateException aggregateException)
            {
                if (aggregateException.InnerException.GetType() == typeof(DocumentClientException))
                {
                    logger.LogError(aggregateException, $"Error in fetching document of type {typeof(T)} with id {id}");
                    DocumentClientException documentException = (DocumentClientException)aggregateException.InnerException;
                    Exception exceptionToThrow = CreateCustomExceptionForDocumentDb(documentException);
                    throw exceptionToThrow;
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Get an item by query
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<T> GetItemByQuery<T>(string query)
        {
            try
            {
                var option = new FeedOptions { EnableCrossPartitionQuery = true };
                var document = documentClient.CreateDocumentQuery<T>(GetDbLink(databaseId) + GetCollectionLink(collectionId), query, option).
                    AsDocumentQuery();

                List<T> documents = new List<T>();
                while (document.HasMoreResults)
                {
                    documents.AddRange(document.ExecuteNextAsync<T>().Result);
                }
                return documents;
            }
            catch (DocumentClientException documentException)
            {
                logger.LogError(documentException, $"Error in fetching document of type {typeof(T)} with query {query}");
                Exception exceptionToThrow = CreateCustomExceptionForDocumentDb(documentException);
                throw exceptionToThrow;
            }
            catch (AggregateException aggregateException)
            {
                if (aggregateException.InnerException.GetType() == typeof(DocumentClientException))
                {
                    logger.LogError(aggregateException, $"Error in fetching document of type {typeof(T)} with id {query}");
                    DocumentClientException documentException = (DocumentClientException)aggregateException.InnerException;
                    Exception exceptionToThrow = CreateCustomExceptionForDocumentDb(documentException);
                    throw exceptionToThrow;
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Get all the items
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> GetAllItems<T>()
        {
            try
            {
                IDocumentQuery<T> query = documentClient.
                    CreateDocumentQuery<T>(UriFactory.CreateDocumentCollectionUri(databaseId, collectionId)).AsDocumentQuery();

                List<T> documents = new List<T>();
                while (query.HasMoreResults)
                {
                    documents.AddRange(query.ExecuteNextAsync<T>().Result);
                }
                return documents;
            }
            catch (DocumentClientException documentException)
            {
                logger.LogError(documentException, $"Error in fetching documents of type {typeof(T)}");
                Exception exceptionToThrow = CreateCustomExceptionForDocumentDb(documentException);
                throw exceptionToThrow;
            }
            catch (AggregateException aggregateException)
            {
                if (aggregateException.InnerException.GetType() == typeof(DocumentClientException))
                {
                    logger.LogError(aggregateException, $"Error in fetching documents of type {typeof(T)}");
                    DocumentClientException documentException = (DocumentClientException)aggregateException.InnerException;
                    Exception exceptionToThrow = CreateCustomExceptionForDocumentDb(documentException);
                    throw exceptionToThrow;
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Set Collection Id
        /// </summary>
        /// <param name="collectionId"></param>
        public void SetCollectionId(string collectionId)
        {
            this.collectionId = collectionId;
        }

        #endregion

        #region Private Method

        private string GetDbLink(string dbName)
        {
            return "dbs/" + dbName;
        }

        private string GetCollectionLink(string collectionName)
        {
            return "/colls/" + collectionName;
        }

        private Exception CreateCustomExceptionForDocumentDb(DocumentClientException exception)
        {
            // To-Do add and throw custom exception
            int statusCode = (int)exception.StatusCode;
            Exception exceptionToThrow = null;
            switch (statusCode)
            {
                case (int)HttpStatusCode.TooManyRequests:
                    {
                        exceptionToThrow = new HttpRequestException(exception.Error.Message);
                        break;
                    }
                case (int)HttpStatusCode.ServiceUnavailable:
                    {
                        exceptionToThrow = new HttpRequestException(exception.Error.Message);
                        break;
                    }
                case (int)HttpStatusCode.NotFound:
                    {
                        exceptionToThrow = new HttpRequestException(exception.Error.Message);
                        break;
                    }
                default:
                    {
                        exceptionToThrow = new HttpRequestException(exception.Error.Message);
                        break;
                    }
            }
            return exceptionToThrow;
        }

        #endregion
    }
}
