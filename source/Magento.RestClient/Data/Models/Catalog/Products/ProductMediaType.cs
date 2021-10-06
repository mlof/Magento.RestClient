using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Magento.RestClient.Data.Models.Catalog.Products
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum ProductMediaType
	{
		[EnumMember(Value = "image")] Image
	}
}