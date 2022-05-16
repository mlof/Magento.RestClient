using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Magento.RestClient.Data.Models.Catalog.Products
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum ProductOptionType
	{
		[EnumMember(Value = "field")] Field,
		[EnumMember(Value = "area")] Area,
		[EnumMember(Value = "file")] File,
		[EnumMember(Value = "drop_down")] DropDown,
		[EnumMember(Value = "radio")] Radio,
		[EnumMember(Value = "checkbox")] Checkbox,
		[EnumMember(Value = "multiple")] Multiple,
		[EnumMember(Value = "date")] Date,
		[EnumMember(Value = "date_time")] DateTime,
		[EnumMember(Value = "time")] Time
	}
}