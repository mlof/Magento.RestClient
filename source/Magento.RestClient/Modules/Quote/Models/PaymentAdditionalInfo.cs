using Newtonsoft.Json;

namespace Magento.RestClient.Modules.Quote.Models
{
	public record PaymentAdditionalInfo
	{
		[JsonProperty("key")] public string Key { get; set; }

		[JsonProperty("value")] public string Value { get; set; }
	}
}