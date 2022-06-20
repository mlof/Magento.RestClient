using Magento.RestClient.Modules.Catalog.Models.Products;
using Newtonsoft.Json;

namespace Magento.RestClient.Requests
{
	public record CreateOrUpdateAttributeRequest
	{
		[JsonProperty("attributeCode")] public string AttributeCode { get; set; }

		[JsonProperty("attribute")] public ProductAttribute Attribute { get; set; }
	}
}