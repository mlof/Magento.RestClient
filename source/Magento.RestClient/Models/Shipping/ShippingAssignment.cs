using System.Collections.Generic;
using Magento.RestClient.Models.Orders;
using Newtonsoft.Json;

namespace Magento.RestClient.Models.Shipping
{
    public record ShippingAssignment
    {
        [JsonProperty("shipping")] public Shipping Shipping { get; set; }

        [JsonProperty("items")] public List<OrderItem> Items { get; set; }
    }
}