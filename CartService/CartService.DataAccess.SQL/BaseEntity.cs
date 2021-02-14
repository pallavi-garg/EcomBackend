using CartService.DataAccess.SQL.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace CartService.DataAccess.SQL
{
    public class BaseEntity : IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
    }
}
