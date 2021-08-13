using Newtonsoft.Json;

namespace Magento.RestClient.Models
{
    public class Shipping
    {
        [JsonProperty("address")] public OrderAddress Address { get; set; }

        [JsonProperty("method")] public string Method { get; set; }

        [JsonProperty("total")] public Total Total { get; set; }
    }
}