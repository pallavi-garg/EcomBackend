namespace Services.Contracts
{
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
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        List<T> GetAllItems<T>();

        /// <summary>
        /// Get an item by query
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        List<T> GetItemByQuery<T>(string query);
    }
}
