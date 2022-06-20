using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Magento.RestClient.Modules.Catalog.Models.Products
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum ProductType
	{
		None,
		[EnumMember(Value = "simple")] Simple,
		[EnumMember(Value = "configurable")] Configurable,
		[EnumMember(Value = "grouped")] Grouped,
		[EnumMember(Value = "virtual")] Virtual,
		[EnumMember(Value = "downloadable")] Downloadable,

		[EnumMember(Value = "bundle")] Bundle
	}
}