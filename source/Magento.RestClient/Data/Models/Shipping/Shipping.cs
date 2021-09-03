using Magento.RestClient.Data.Models.Orders;
using Newtonsoft.Json;

namespace Magento.RestClient.Data.Models.Shipping
{
    public record Shipping
    {
        [JsonProperty("address")] public OrderAddress Address { get; set; }

        [JsonProperty("method")] public string Method { get; set; }

        [JsonProperty("total")] public Total Total { get; set; }
    }
}