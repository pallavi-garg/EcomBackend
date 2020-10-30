using ProductService.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductService.DataAccess
{
    public partial class DataAccessBridge : IBaseDataAccessBridge
    {
        // Repository pattern
        // private readonly IRepository Repo;
        //public DataAccessBridge(IRepository repo)
        //{

        //}

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
