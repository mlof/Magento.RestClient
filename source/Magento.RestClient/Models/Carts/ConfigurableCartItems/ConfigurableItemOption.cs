using Newtonsoft.Json;

namespace Magento.RestClient.Models.Carts.ConfigurableCartItems
{
	public record ConfigurableItemOption
	{
		[JsonProperty("option_id")] public string OptionId { get; set; }
		[JsonProperty("option_value")] public long OptionValue { get; set; }
	}
}