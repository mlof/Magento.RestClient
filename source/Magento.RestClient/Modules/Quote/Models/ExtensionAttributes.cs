using System.Collections.Generic;
using Newtonsoft.Json;

namespace Magento.RestClient.Modules.Quote.Models
{
	public record ExtensionAttributes
	{
		[JsonProperty("discounts")] public List<Discount> Discounts { get; set; }
	}
}