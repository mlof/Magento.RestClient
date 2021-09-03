using System.Collections.Generic;
using Newtonsoft.Json;

namespace Magento.RestClient.Data.Models.Payments
{
    public record Payment
    {
        [JsonProperty("account_status")] public dynamic AccountStatus { get; set; }

        [JsonProperty("additional_information")]
        public List<string> AdditionalInformation { get; set; }

        [JsonProperty("amount_ordered")] public long AmountOrdered { get; set; }

        [JsonProperty("base_amount_ordered")] public long BaseAmountOrdered { get; set; }

        [JsonProperty("base_shipping_amount")] public long BaseShippingAmount { get; set; }

        [JsonProperty("cc_exp_year")] public long CcExpYear { get; set; }

        [JsonProperty("cc_last4")] public dynamic CcLast4 { get; set; }

        [JsonProperty("cc_ss_start_month")] public long CcSsStartMonth { get; set; }

        [JsonProperty("cc_ss_start_year")] public long CcSsStartYear { get; set; }

        [JsonProperty("entity_id")] public long EntityId { get; set; }

        [JsonProperty("method")] public string Method { get; set; }

        [JsonProperty("parent_id")] public long ParentId { get; set; }

        [JsonProperty("shipping_amount")] public long ShippingAmount { get; set; }
    }
}