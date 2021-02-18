using System.Collections.Generic;

namespace CartService.DataAccess.SQL.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        T GetById(string id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(string id);
        void Save();
        List<T> GetByCartId(string cartId);
        void DeleteByCartId(string cartId);
    }
}
