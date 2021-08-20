using Newtonsoft.Json;

namespace Magento.RestClient.Models.Search
{
    public record Filter
    {
        [JsonProperty("field")] public string Field { get; set; }
        [JsonProperty("value")] public string Value { get; set; }
        [JsonProperty("condition_type")] public string ConditionType { get; set; }
    }
}