using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.BusinessLogic.Interfaces
{
    public interface IProductDetailsProvider
    {
        Task<string> GetProducts();
    }
}
