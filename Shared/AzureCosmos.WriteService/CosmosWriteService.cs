namespace AzureCosmos.WriteService
{
    using Microsoft.Azure.Documents;
    using Microsoft.Azure.Documents.Client;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using Services.Contracts;
    using System;
    using System.Net;
    using System.Net.Http;

    public class CosmosWriteService : IWriteService
    {
        #region Private Members

        private readonly string databaseId;
        private string collectionId;
        private readonly ILogger<CosmosWriteService> logger;
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
        public CosmosWriteService(IConfiguration configuration, string cosmosEndpointUrl, string cosmosKey, string cosmosDatabaseId,
            ILogger<CosmosWriteService> logger)
        {
            documentClient = new DocumentClient(new Uri(configuration[cosmosEndpointUrl]), configuration[cosmosKey]);
            databaseId = configuration[cosmosDatabaseId];
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
            try
            {
                var document = documentClient.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(databaseId, collectionId, id), item).Result;
                return JsonConvert.DeserializeObject<T>(document.Resource.ToString());
            }
            catch (DocumentClientException documentException)
            {
                logger.LogError(documentException, $"Error in updating document of type {typeof(T)} with id {id}");
                Exception exceptionToThrow = CreateCustomExceptionForDocumentDb(documentException);
                throw exceptionToThrow;
            }
            catch (AggregateException aggregateException)
            {
                if (aggregateException.InnerException.GetType() == typeof(DocumentClientException))
                {
                    logger.LogError(aggregateException, $"Error in updating document of type {typeof(T)} with id {id}");
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
        /// Add new item
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>

        public T AddItem<T>(T item)
        {
            try
            {
                var document = documentClient.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(databaseId, collectionId), item, null, true).Result;
                return JsonConvert.DeserializeObject<T>(document.Resource.ToString());
            }
            catch (DocumentClientException documentException)
            {
                logger.LogError(documentException, $"Error in adding document of type {typeof(T)}");
                Exception exceptionToThrow = CreateCustomExceptionForDocumentDb(documentException);
                throw exceptionToThrow;
            }
            catch (AggregateException aggregateException)
            {
                if (aggregateException.InnerException.GetType() == typeof(DocumentClientException))
                {
                    logger.LogError(aggregateException, $"Error in adding document of type {typeof(T)}");
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
        /// Delete an item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteItem(string id)
        {
            bool isDeleted = false;
            try
            {
                documentClient.DeleteDocumentAsync(UriFactory.CreateDocumentUri(databaseId, collectionId, id));
                isDeleted = true;
            }
            catch (DocumentClientException documentException)
            {
                logger.LogError(documentException, $"Error in deleting document with id {id}");
                Exception exceptionToThrow = CreateCustomExceptionForDocumentDb(documentException);
                throw exceptionToThrow;
            }
            catch (AggregateException aggregateException)
            {
                if (aggregateException.InnerException.GetType() == typeof(DocumentClientException))
                {
                    logger.LogError(aggregateException, $"Error in deleting document with id {id}");
                    DocumentClientException documentException = (DocumentClientException)aggregateException.InnerException;
                    Exception exceptionToThrow = CreateCustomExceptionForDocumentDb(documentException);
                    throw exceptionToThrow;
                }
                else
                {
                    throw;
                }
            }

            return isDeleted;
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
