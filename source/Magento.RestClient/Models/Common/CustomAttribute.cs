using Newtonsoft.Json;

namespace Magento.RestClient.Models.Common
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