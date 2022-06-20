using Magento.RestClient.Modules.Order.Models;
using Newtonsoft.Json;

namespace Magento.RestClient.Modules.Sales.Models
{
	public record Shipping
	{
		[JsonProperty("address")] public OrderAddress Address { get; set; }

		[JsonProperty("method")] public string Method { get; set; }

		[JsonProperty("total")] public Total Total { get; set; }
	}
}