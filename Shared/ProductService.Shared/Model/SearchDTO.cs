using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ProductService.Shared
{
    public class SearchDTO
    {
        [DataMember]
        public string Key { get; set; }

        [DataMember]
        public List<string> Value { get; set; }
    }
}
