using Newtonsoft.Json;

namespace Magento.RestClient.Models.Carts.ConfigurableCartItems
{
	public record ConfigurableCartItemProductOption
	{
		public ConfigurableCartItemProductOption()
		{
			this.ExtensionAttributes = new ConfigurableCartItemProductOptionExtensionAttributes();
		}

		[JsonProperty("extension_attributes")]
		public ConfigurableCartItemProductOptionExtensionAttributes ExtensionAttributes { get; set; }
	}
}