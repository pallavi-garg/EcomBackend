using System;
using System.ComponentModel.DataAnnotations;

namespace CartService.DataAccess.SQL.Interfaces
{
    public interface IEntity
    {
        public DateTime CreatedOn { get; set; }
    }
}
