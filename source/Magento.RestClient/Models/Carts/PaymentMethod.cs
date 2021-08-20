using Newtonsoft.Json;

namespace Magento.RestClient.Models.Carts
{
    public record PaymentMethod
    {
        [JsonProperty("code")] public string Code { get; set; }
        [JsonProperty("title")] public string Title { get; set; }
    }
}