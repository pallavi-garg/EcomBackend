namespace Services.Contracts
{
    using ProductService.Shared;
    using System.Collections.Generic;

    public interface IReadService
    {
        /// <summary>
        /// Get an item by Id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetItemById<T>(string id);

        /// <summary>
        /// Get all the items
        /// </summary>
        /// <param name="continuationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        SearchResult<T> GetAllItems<T>(string continuationToken);

        /// <summary>
        /// Get an item by query
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="continuationToken"></param>
        /// <returns></returns>
        SearchResult<T> GetItemByQuery<T>(string query, string continuationToken);

        int GetItemCountByQuery(string query);
    }
}
