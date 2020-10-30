using System;
using System.Collections.Generic;
using System.Text;

namespace ProductService.DataAccess.Interfaces
{
    public interface IBaseDataAccessBridge: IProductDetailDataAccessBridge
    {
        void SaveChanges();
    }
}
