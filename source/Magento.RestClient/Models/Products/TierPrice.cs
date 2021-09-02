using JsonExts.JsonPath;
using Magento.RestClient.Converters;
using Newtonsoft.Json;

namespace Magento.RestClient.Models.Products
{
    [JsonConverter(typeof(JsonPathObjectConverter))]
    public record TierPrice
    {
        [JsonProperty("customer_group_id")] public long CustomerGroupId { get; set; }

        [JsonProperty("qty")] public long Qty { get; set; }

        [JsonProperty("value")] public long Value { get; set; }

        [JsonPath("extension_attributes.website_id")]
        public int WebsiteId { get; set; }
    }
}