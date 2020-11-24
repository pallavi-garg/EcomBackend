using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ProductService.Shared.Model
{
    [DataContract]
    public class FeatureDetail
    {
        [DataMember]
        public string Key { get; set; }
        
        [DataMember]
        public string Value { get; set; }
    }
}
