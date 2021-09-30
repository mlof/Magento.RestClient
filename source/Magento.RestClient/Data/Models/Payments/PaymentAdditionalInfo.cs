using Newtonsoft.Json;

namespace Magento.RestClient.Data.Models.Payments
{
	public record PaymentAdditionalInfo
	{
		[JsonProperty("key")] public string Key { get; set; }

		[JsonProperty("value")] public string Value { get; set; }
	}
}