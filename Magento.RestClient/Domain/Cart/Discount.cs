using Newtonsoft.Json;

namespace Magento.RestClient.Domain
{
    public partial class Discount
    {
        [JsonProperty("discount_data")] public DiscountData DiscountData { get; set; }

        [JsonProperty("rule_label")] public string RuleLabel { get; set; }

        [JsonProperty("rule_id")] public long RuleId { get; set; }
    }
}