using Newtonsoft.Json;

namespace Magento.RestClient.Domain
{
    public class PaymentMethod
    {
        [JsonProperty("code")] public string Code { get; set; }
        [JsonProperty("title")] public string Title { get; set; }
    }
}