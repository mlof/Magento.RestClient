using Newtonsoft.Json;

namespace Magento.RestClient.Data.Models.Inventory
{
	public record CarrierLink
	{
		[JsonProperty("carrier_code")] public string CarrierCode { get; set; }

		[JsonProperty("position")] public long Position { get; set; }

		[JsonProperty("extension_attributes")] public dynamic ExtensionAttributes { get; set; }
	}
}