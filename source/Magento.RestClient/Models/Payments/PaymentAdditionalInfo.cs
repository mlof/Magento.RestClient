using Newtonsoft.Json;

namespace Magento.RestClient.Models.Payments
{
    public class PaymentAdditionalInfo
    {
        [JsonProperty("key")] public string Key { get; set; }

        [JsonProperty("value")] public string Value { get; set; }
    }
}