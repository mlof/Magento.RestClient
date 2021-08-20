using Newtonsoft.Json;

namespace Magento.RestClient.Models.Search
{
    public record SortOrder
    {
        [JsonProperty("field")] public string Field { get; set; }
        [JsonProperty("direction")] public string Direction { get; set; }

    }
}