using System.Collections.Generic;
using Newtonsoft.Json;

namespace Magento.RestClient.Data.Models
{
	public record ProductMedia
	{
		[JsonProperty("id")] public long Id { get; set; }

		[JsonProperty("media_type")] public ProductMediaType? MediaType { get; set; }

		[JsonProperty("label")] public string Label { get; set; }

		[JsonProperty("position")] public long Position { get; set; }

		[JsonProperty("disabled")] public bool Disabled { get; set; }

		[JsonProperty("types")] public List<string> Types { get; set; }

		[JsonProperty("file")] public string File { get; set; }

		[JsonProperty("content")] public ProductMediaContent Content { get; set; }
	}
}