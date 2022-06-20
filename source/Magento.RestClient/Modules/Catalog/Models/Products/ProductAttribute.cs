using Magento.RestClient.Modules.EAV.Model;

namespace Magento.RestClient.Modules.Catalog.Models.Products
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
			this.IsRequired = isRequired;
			this.EntityTypeId = EntityType.CatalogProduct;
		}

		public bool IsVisible { get; set; }
	}
}