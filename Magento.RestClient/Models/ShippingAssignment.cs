using System.Collections.Generic;
using Newtonsoft.Json;

namespace Magento.RestClient.Models
{
    public class ShippingAssignment
    {
        [JsonProperty("shipping")] public Shipping Shipping { get; set; }

        [JsonProperty("items")] public List<OrderItem> Items { get; set; }
    }
}