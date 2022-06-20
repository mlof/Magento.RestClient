using Newtonsoft.Json;

namespace Magento.RestClient.Modules.Directory.Models
{
	public record Region
	{
		[JsonProperty("region_code")] public string RegionCode { get; set; }

		[JsonProperty("region")] public string RegionRegion { get; set; }

		[JsonProperty("region_id")] public long RegionId { get; set; }
	}
}