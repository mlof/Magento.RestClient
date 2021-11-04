using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Magento.RestClient.Data.Models.Catalog.Products
{
	[JsonConverter(typeof(StringEnumConverter))]

	public enum PriceType
	{
		[EnumMember(Value = "fixed")] Fixed,
		[EnumMember(Value = "dynamic")] Dynamic
	}
}