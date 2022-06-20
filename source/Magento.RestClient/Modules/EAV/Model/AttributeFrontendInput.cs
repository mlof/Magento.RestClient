using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Magento.RestClient.Modules.EAV.Model
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum AttributeFrontendInput
	{
		///<Summary>Text Field </Summary>
		[EnumMember(Value = "text")] Text,

		///<Summary>Text Area </Summary>
		[EnumMember(Value = "textarea")] Textarea,

		///<Summary>Text Editor </Summary>
		[EnumMember(Value = "texteditor")] Texteditor,

		///<Summary>Date </Summary>
		[EnumMember(Value = "date")] Date,

		///<Summary>Date and Time </Summary>
		[EnumMember(Value = "datetime")] Datetime,

		///<Summary>Yes/No </Summary>
		[EnumMember(Value = "boolean")] Boolean,

		///<Summary>Multiple Select </Summary>
		[EnumMember(Value = "multiselect")] Multiselect,

		///<Summary>Dropdown </Summary>
		[EnumMember(Value = "select")] Select,

		///<Summary>Price </Summary>
		[EnumMember(Value = "price")] Price,

		///<Summary>Media Image </Summary>
		[EnumMember(Value = "media_image")] MediaImage,

		///<Summary>Visual Swatch </Summary>
		[EnumMember(Value = "swatch_visual")] SwatchVisual,

		///<Summary>Text Swatch </Summary>
		[EnumMember(Value = "swatch_text")] SwatchText,

		///<Summary>Fixed Product Tax </Summary>
		[EnumMember(Value = "weee")] Weee,
		[EnumMember(Value = "gallery")] Gallery,
		[EnumMember(Value = "weight")] Weight
	}
}