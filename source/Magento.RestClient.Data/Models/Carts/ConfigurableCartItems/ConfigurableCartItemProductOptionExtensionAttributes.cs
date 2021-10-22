using System.Collections.Generic;
using Newtonsoft.Json;

namespace Magento.RestClient.Data.Models.Carts.ConfigurableCartItems
{
	public record ConfigurableCartItemProductOptionExtensionAttributes
	{
		public ConfigurableCartItemProductOptionExtensionAttributes()
		{
			this.ConfigurableItemOptions = new List<ConfigurableItemOption>();
		}

		[JsonProperty("configurable_item_options")]

		public List<ConfigurableItemOption> ConfigurableItemOptions { get; set; }
	}
}