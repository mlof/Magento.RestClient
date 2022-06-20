using System.Collections.Generic;
using Newtonsoft.Json;

namespace Magento.RestClient.Modules.Common.Models
{
	public record ValidationResult
	{
		[JsonProperty("valid")] public bool Valid { get; set; }

		[JsonProperty("messages")] public List<string> Messages { get; set; }
	}
}