using Newtonsoft.Json;

namespace MagentoApi.Models
{
    public partial class Region
    {
        [JsonProperty("region_code")] public string RegionCode { get; set; }

        [JsonProperty("region")] public string RegionRegion { get; set; }

        [JsonProperty("region_id")] public long RegionId { get; set; }
    }
}