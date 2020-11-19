using System;
using System.Collections.Generic;
using System.Text;

namespace ProductService.DataAccess.SQL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Linq.Expressions;
    using Microsoft.EntityFrameworkCore;
    using ProductService.DataAccess.SQL.Interfaces;

    /// <summary>
    /// Provide database interaction in form of entities and functionality to save changes to database.
    /// </summary>
    public sealed class Repository : IRepository
    {

        #region Public methods
        /// <summary>
        /// Initializes a new instance of the <see cref="Repository"/> class.
        /// </summary>
        /// <param name="context">The underlying context on which the repository operates.</param>
        public Repository()
        {
        }



        /// <inheritdoc />
        public void Add<T>(T entity) where T : class, Identifiable, IFingerPrint
        {
            currentContext.Set<T>().Add(entity);
        }




        /// <inheritdoc />
        public void Delete<T>(T entity) where T : class, Identifiable, IFingerPrint
        {
            currentContext.Set<T>().Remove(entity);
        }



        /// <inheritdoc />
        public void DeleteRange<T>(IEnumerable<T> entities) where T : class, Identifiable, IFingerPrint
        {
            currentContext.Set<T>().RemoveRange(entities);
        }



        /// <inheritdoc />
        public T FindById<T>(Guid id) where T : class, Identifiable, IFingerPrint
        {
            return currentContext.Set<T>().Find(id);
        }



        /// <inheritdoc />
        public T Get<T>(Expression<Func<T, bool>> predicate) where T : class, Identifiable, IFingerPrint
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            // return currentContext.Set<T>().Where(predicate).Where(GetMandantCheck<T>()).FirstOrDefault();
        }

        public T GetForAllMandants<T>(Expression<Func<T, bool>> predicate) where T : class, Identifiable, IFingerPrint
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return currentContext.Set<T>().Where(predicate).FirstOrDefault();
        }



        /// <inheritdoc />
        public IQueryable<T> GetAll<T>(Expression<Func<T, bool>> predicate) where T : class, Identifiable, IFingerPrint
        {
            return currentContext.Set<T>().Where(predicate).Where(GetMandantCheck<T>());
        }



        /// <inheritdoc />
        public IQueryable<T> GetAllForAllMandants<T>(Expression<Func<T, bool>> predicate) where T : class, Identifiable, IFingerPrint
        {
            return currentContext.Set<T>().Where(predicate);
        }

        /// <summary>
        /// Implementing dispose functionality
        /// </summary>
        public void Dispose() => currentContext.Dispose();

        /// <inheritdoc />
        public int SaveChanges() => currentContext.SaveChanges();

        public void AddRange<T>(IEnumerable<T> entities) where T : class, Identifiable, IFingerPrint => currentContext.AddRange(entities);

        public void UpdateRange<T>(IEnumerable<T> entities) where T : class, Identifiable, IFingerPrint => currentContext.UpdateRange(entities);

        #endregion

    }
}