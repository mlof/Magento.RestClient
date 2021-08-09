using Newtonsoft.Json;

namespace MagentoApi.Models
{
    public class CustomAttribute
    {
        [JsonProperty("attribute_code")] public string AttributeCode { get; set; }
        [JsonProperty("value")] public dynamic Value { get; set; }
    }
}