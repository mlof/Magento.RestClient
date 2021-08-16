using System.Collections.Generic;
using Newtonsoft.Json;

namespace Magento.RestClient.Models.Carts
{
    public partial class ExtensionAttributes
    {
        [JsonProperty("discounts")] public List<Discount> Discounts { get; set; }
    }
}