using System.Collections.Generic;
using Magento.RestClient.Data.Models.Attributes;
using Magento.RestClient.Data.Models.Common;
using Newtonsoft.Json;

namespace Magento.RestClient.Data.Models.Products
{
	public record ProductAttribute : EntityAttribute
	{
		public ProductAttribute()
		{
		}

		public ProductAttribute(string code, AttributeFrontendInput frontendInput = AttributeFrontendInput.Text,
			bool isRequired = false)
		{
			this.AttributeCode = code;
			this.FrontendInput = frontendInput;
			this.IsRequired = false;
			this.EntityTypeId = EntityType.CatalogProduct;
		}

		public bool IsVisible { get; set; }
	}
}