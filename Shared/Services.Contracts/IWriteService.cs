namespace Services.Contracts
{
    using System.Collections.Generic;

    public interface IWriteService
    {
        /// <summary>
        /// Update an item
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        T UpdateItem<T>(string id, T item);

        /// <summary>
        /// Add new item
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>

        T AddItem<T>(T item);

        /// <summary>
        /// Delete an item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool DeleteItem(string id);

        /// <summary>
        /// Set Collection Id
        /// </summary>
        /// <param name="collectionId"></param>
        void SetCollectionId(string collectionId);
    }
}
