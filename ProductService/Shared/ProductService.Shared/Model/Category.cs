using ProductService.Shared.Model;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ProductService.Shared
{
    [DataContract]
    public class Category
    {
        [DataMember(Name = "id")]
        public Guid? Id { get; set; }

        [DataMember]
        public string CategoryId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string[] SuperCategory { get; set; }

        [DataMember]
        public string Department { get; set; }

        public DateTime CreateDate { get; set; }
        
        [DataMember]
        public DateTime ModifiedDate { get; set; }
        
    }

}
