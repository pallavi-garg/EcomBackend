using System;
using System.ComponentModel.DataAnnotations;

namespace CartService.DataAccess.SQL.Interfaces
{
    public interface IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
