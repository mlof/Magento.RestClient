using System.Collections.Generic;
using Newtonsoft.Json;

namespace Magento.RestClient.Models
{
    public class FilterGroup
    {
        [JsonProperty("filters")] public List<Filter> Filters { get; set; }
    }
}