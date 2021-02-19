using System;
using System.ComponentModel.DataAnnotations;

namespace CartService.DataAccess.SQL
{
    public class Cart: BaseEntity
    {

        public string CustomerId { get; set; }
}
}
