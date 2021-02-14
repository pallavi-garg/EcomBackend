using System;

namespace CartService.DataAccess.SQL
{
    public class CartProductMapping: BaseEntity
    {
        public string CartId { get; set; }
        public string ProductId { get; set; }
        public string SKU { get; set; }
    }
}
