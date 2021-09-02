using System.Collections.Generic;
using Newtonsoft.Json;

namespace Magento.RestClient.Models.Carts
{
	public record ExtensionAttributes
	{
		[JsonProperty("discounts")] public List<Discount> Discounts { get; set; }
	}
}