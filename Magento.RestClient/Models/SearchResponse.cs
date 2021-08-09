using System.Collections.Generic;
using Newtonsoft.Json;

namespace MagentoApi.Models
{
    public class SearchResponse<T>
    {
        [JsonProperty("items")] public List<T> Items { get; set; }
        [JsonProperty("search_criteria")] public SearchCriteria SearchCriteria { get; set; }
        [JsonProperty("total_count")] public int TotalCount { get; set; }
    }
}