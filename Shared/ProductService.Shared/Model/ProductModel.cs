using ProductService.Shared.Model;
using System;
using System.Runtime.Serialization;

namespace ProductService.Shared
{
    [DataContract]
    public class ProductModel
    {
        [DataMember(Name = "id")]
        public Guid? Id { get; set; }

        [DataMember]
        public string ProductId { get; set; }

        [DataMember]
        public string ShortDescription { get; set; }

        [DataMember]
        public string LongDescription { get; set; }

        [DataMember]
        public string[] SuperCategory { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Department { get; set; }

        [DataMember]
        public string Brand { get; set; }
        
        [DataMember]
        public string Sku { get; set; }
        
        [DataMember]
        public string Price { get; set; }
        
        [DataMember]
        public DateTime CreateDate { get; set; }
        
        [DataMember]
        public DateTime ModifiedDate { get; set; }
        
        [DataMember]
        public ImageDetail[] Media { get; set; }
        
        [DataMember]
        public FeatureDetail[] Features { get; set; }
    }

}
