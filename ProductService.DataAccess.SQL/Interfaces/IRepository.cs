using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ProductService.DataAccess.SQL.Interfaces
{
    public interface IRepository : IDisposable
    {



        /// <summary>
        /// Gets all entities of type T satisfying given predicate
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate">The predicate.</param>
        /// <returns>IQueryable{``0}.</returns>
        IQueryable<T> GetAll<T>(Expression<Func<T, bool>> predicate) where T : class, Identifiable, IFingerPrint;



        /// <summary>
        /// Gets all entities of type T satisfying given predicate and independent of logged-in mandant
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate">The predicate.</param>
        /// <returns>IQueryable{``0}.</returns>
        IQueryable<T> GetAllForAllMandants<T>(Expression<Func<T, bool>> predicate) where T : class, Identifiable, IFingerPrint;



        /// <summary>
        /// Gets the first matching entity satisfying given predicate
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate">The predicate.</param>
        /// <returns>``0.</returns>



        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
        T Get<T>(Expression<Func<T, bool>> predicate) where T : class, Identifiable, IFingerPrint;



        /// <summary>
        /// Gets the first matching entity satisfying given predicate and independent of logged-in mandant
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate">The predicate.</param>
        /// <returns>``0.</returns>



        T GetForAllMandants<T>(Expression<Func<T, bool>> predicate) where T : class, Identifiable, IFingerPrint;



        /// <summary>
        /// Gets the first matching entity satisfying given Id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">The keyValue.</param>
        /// <returns>``0.</returns>
        T FindById<T>(Guid id) where T : class, Identifiable, IFingerPrint;



        /// <summary>
        /// Adds the entity passed to data context.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        void Add<T>(T entity) where T : class, Identifiable, IFingerPrint;



        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        void Delete<T>(T entity) where T : class, Identifiable, IFingerPrint;



        /// <summary>
        /// delete list of entities
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"> entities</param>
        void DeleteRange<T>(IEnumerable<T> entities) where T : class, Identifiable, IFingerPrint;



        /// <summary>
        /// Deletes all question blocks and their child question for advisory path and next blocks after given block
        /// </summary>
        /// <param name="questipnBlockIds"></param>
        void DeleteSubsequentQuestionBlocks(Guid advisoryPthId, int numBlock);



        /// <summary>
        /// delete list of entities
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"> entities</param>
        void AddRange<T>(IEnumerable<T> entities) where T : class, Identifiable, IFingerPrint;



        /// <summary>
        /// This would be responsible to update the set of records collectively in the database
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        void UpdateRange<T>(IEnumerable<T> entities) where T : class, Identifiable, IFingerPrint;



        /// <summary>
        /// Saves the changes made to the data context.
        /// </summary>       
        /// <returns>The number of objects written to the underlying database.</returns>
        int SaveChanges();



    }
}
