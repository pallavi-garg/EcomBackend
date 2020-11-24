using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ProductService.Shared.Model
{
    [DataContract]
    public class ImageDetail
    {
        [DataMember]
        public string ThumbnailUrl { get; set; }
        
        [DataMember]
        public string BaseUrl { get; set; }
    }
}
