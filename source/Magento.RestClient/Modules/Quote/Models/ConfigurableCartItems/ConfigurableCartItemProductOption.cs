using Newtonsoft.Json;

namespace Magento.RestClient.Modules.Quote.Models.ConfigurableCartItems
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