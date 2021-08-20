using System;
using Newtonsoft.Json;

namespace Magento.RestClient.Models.Orders
{
    public record StatusHistory
    {
        [JsonProperty("comment")] public string Comment { get; set; }

        [JsonProperty("created_at")] public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("entity_id")] public long EntityId { get; set; }

        [JsonProperty("entity_name")] public string EntityName { get; set; }

        [JsonProperty("is_customer_notified")] public long IsCustomerNotified { get; set; }

        [JsonProperty("is_visible_on_front")] public long IsVisibleOnFront { get; set; }

        [JsonProperty("parent_id")] public long ParentId { get; set; }

        [JsonProperty("status")] public string Status { get; set; }
    }
}