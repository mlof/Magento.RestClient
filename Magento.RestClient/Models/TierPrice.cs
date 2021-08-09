using MagentoApi.Converters;
using Newtonsoft.Json;

namespace MagentoApi.Models
{
    [JsonConverter(typeof(JsonPathConverter))]
    public class TierPrice
    {
        [JsonProperty("customer_group_id")] public long CustomerGroupId { get; set; }

        [JsonProperty("qty")] public long Qty { get; set; }

        [JsonProperty("value")] public long Value { get; set; }

        [JsonProperty("extension_attributes.website_id")]
        public int WebsiteId { get; set; }
    }
}