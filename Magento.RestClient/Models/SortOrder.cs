using Newtonsoft.Json;

namespace MagentoApi.Models
{
    public class SortOrder
    {
        [JsonProperty("field")] public string Field { get; set; }
        [JsonProperty("direction")] public string Direction { get; set; }

    }
}