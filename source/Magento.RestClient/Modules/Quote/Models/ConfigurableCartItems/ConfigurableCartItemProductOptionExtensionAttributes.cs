using System.Collections.Generic;
using Newtonsoft.Json;

namespace Magento.RestClient.Modules.Quote.Models.ConfigurableCartItems
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