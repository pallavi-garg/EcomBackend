using System;
using System.Runtime.Serialization;

namespace ProductService.Shared
{
    [DataContract]
    public class ProductModel
    {
        /// <summary>
        /// Unique document Id for cosmos
        /// </summary>
        [DataMember(Name = "id")]
        public Guid? Id { get; set; }

        [DataMember]
        public string ProductId { get; set; }
        [DataMember]
        public string ProductName { get; set; }
        [DataMember]
        public string ProductCategory { get; set; }
        [DataMember]
        public long Price { get; set; }
        [DataMember]
        public bool IsAvailable { get; set; }
    }
}
