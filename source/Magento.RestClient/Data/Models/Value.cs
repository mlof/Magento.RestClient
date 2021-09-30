using Newtonsoft.Json;

namespace Magento.RestClient.Data.Models
{
	public partial class Value
	{
		[JsonProperty("value_index")] public long ValueIndex { get; set; }
	}
}