using Newtonsoft.Json;

namespace Magento.RestClient.Modules.Common.Models
{
	public record CustomAttribute
	{
		public CustomAttribute()
		{
		}

		public CustomAttribute(string attributeCode, dynamic value)
		{
			this.AttributeCode = attributeCode;
			this.Value = value;
		}

		[JsonProperty("attribute_code")] public string AttributeCode { get; set; }
		[JsonProperty("value")] public dynamic Value { get; set; }
	}
}