using CartService.DataAccess.SQL.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace CartService.DataAccess.SQL
{
    public class BaseEntity : IEntity
    {
        public string Id { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;
    }
}
