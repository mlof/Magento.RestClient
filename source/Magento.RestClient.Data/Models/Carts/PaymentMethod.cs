using Newtonsoft.Json;

namespace Magento.RestClient.Data.Models.Carts
{
	public record PaymentMethod
	{
		[JsonProperty("code")] public string Code { get; set; }
		[JsonProperty("title")] public string Title { get; set; }
	}
}