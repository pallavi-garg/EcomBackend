using System;
using System.Collections.Generic;
using System.Text;

namespace ProductService.DataAccess
{
    public interface IBaseDataAccessBridge: IProductDetailDataAccessBridge
    {
        void SaveChanges();
    }
}
