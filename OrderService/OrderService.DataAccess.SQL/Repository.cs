﻿namespace OrderService.DataAccess
{
    using Microsoft.EntityFrameworkCore;
    using OrderService.DataAccess.SQL;
    using OrderService.DataAccess.SQL.Interfaces;
    using OrderService.Shared.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;


    /// <summary>
    /// Provide database interaction in form of entities and functionality to save changes to database.
    /// </summary>
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {

        private readonly DBContext _context;
        private DbSet<T> entities;
        public Repository(DBContext context)
        {
            this._context = context;
            entities = context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }
        public T GetById(string id)
        {
            return entities.SingleOrDefault(s => s.Id.ToString() == id);
        }
        public void Insert(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");

            entities.Add(entity);
            Save();
        }
        public void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _context.Entry(entity).State = EntityState.Modified;
            Save();
        }
        public void Delete(string id)
        {
            if (id == null) throw new ArgumentNullException("entity");

            T entity = entities.FirstOrDefault(s => s.Id.ToString() == id);
            if(entity != null)
            {

                entities.Remove(entity);
                Save();
            }
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}