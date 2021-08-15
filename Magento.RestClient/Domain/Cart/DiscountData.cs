using Newtonsoft.Json;

namespace Magento.RestClient.Domain.Cart
{
    public partial class DiscountData
    {
        [JsonProperty("amount")] public long Amount { get; set; }

        [JsonProperty("base_amount")] public long BaseAmount { get; set; }

        [JsonProperty("original_amount")] public long OriginalAmount { get; set; }

        [JsonProperty("base_original_amount")] public long BaseOriginalAmount { get; set; }
    }
}