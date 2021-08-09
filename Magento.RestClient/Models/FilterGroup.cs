using System.Collections.Generic;
using Newtonsoft.Json;

namespace MagentoApi.Models
{
    public class FilterGroup
    {
        [JsonProperty("filters")] public List<Filter> Filters { get; set; }
    }
}