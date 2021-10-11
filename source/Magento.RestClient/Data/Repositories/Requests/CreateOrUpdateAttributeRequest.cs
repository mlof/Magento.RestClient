using Magento.RestClient.Data.Models.Catalog.Products;
using Newtonsoft.Json;

namespace Magento.RestClient.Data.Repositories
{
	public record CreateOrUpdateAttributeRequest
	{
		[JsonProperty("attributeCode")]
		public string AttributeCode { get; set; }
		[JsonProperty("attribute")]

		public ProductAttribute Attribute { get; set; }
	}
}