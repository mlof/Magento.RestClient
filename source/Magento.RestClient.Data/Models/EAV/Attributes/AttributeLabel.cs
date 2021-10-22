using Newtonsoft.Json;

namespace Magento.RestClient.Data.Models.EAV.Attributes
{
	public record AttributeLabel
	{
		[JsonProperty("store_id")] public int StoreId { get; set; }
		[JsonProperty("label")] public string Label { get; set; }
	}
}