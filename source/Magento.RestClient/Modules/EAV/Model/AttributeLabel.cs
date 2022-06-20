using Newtonsoft.Json;

namespace Magento.RestClient.Modules.EAV.Model
{
	public record AttributeLabel
	{
		[JsonProperty("store_id")] public int StoreId { get; set; }
		[JsonProperty("label")] public string Label { get; set; }
	}
}