using System.Collections.Generic;
using JsonExts.JsonPath;
using Newtonsoft.Json;

namespace Magento.RestClient.Data.Models.Carts.ConfigurableCartItems
{
	[JsonConverter(typeof(JsonPathObjectConverter))]
	public record ConfigurableCartItem : CartItem
	{
		public ConfigurableCartItem()
		{
			this.ProductOption = new ConfigurableCartItemProductOption();
		}

		[JsonProperty("product_option")] public ConfigurableCartItemProductOption ProductOption { get; set; }

		[JsonIgnore]
		public List<ConfigurableItemOption> ConfigurableItemOptions =>
			this.ProductOption.ExtensionAttributes.ConfigurableItemOptions;
	}
}