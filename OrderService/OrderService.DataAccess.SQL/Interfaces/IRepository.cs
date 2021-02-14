﻿using OrderService.Shared.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.DataAccess.SQL.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        T GetById(string id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(string id);
        void Save();
    }
}
