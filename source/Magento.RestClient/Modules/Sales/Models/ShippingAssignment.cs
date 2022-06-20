using System.Collections.Generic;
using Magento.RestClient.Modules.Order.Models;
using Newtonsoft.Json;

namespace Magento.RestClient.Modules.Sales.Models
{
	public record ShippingAssignment
	{
		[JsonProperty("shipping")] public Shipping Shipping { get; set; }

		[JsonProperty("items")] public List<OrderItem> Items { get; set; }
	}
}