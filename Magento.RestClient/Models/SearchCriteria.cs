using System.Collections.Generic;
using Newtonsoft.Json;

namespace MagentoApi.Models
{
    public class SearchCriteria
    {
        [JsonProperty("filter_groups")] public List<FilterGroup> FilterGroups { get; set; }
        [JsonProperty("sort_orders")] public List<SortOrder> SortOrders { get; set; }
        [JsonProperty("page_size")] public int PageSize { get; set; }
        [JsonProperty("current_page")] public int CurrentPage { get; set; }
    }
}