using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ProductService.Shared
{
    [DataContract]
    public class SearchResult<T>
    {
        [DataMember]
        public List<T> Data { get; set; }

        [DataMember]
        public string ContinuationToken { get; set; }

        [DataMember]
        public int TotalCount { get; set; }
    }
}
