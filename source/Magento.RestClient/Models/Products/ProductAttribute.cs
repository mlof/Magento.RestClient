using Magento.RestClient.Domain;
using Magento.RestClient.Models.Attributes;
using Magento.RestClient.Models.Common;

namespace Magento.RestClient.Models.Products
{
    public record ProductAttribute : EntityAttribute
    {
        public ProductAttribute()
        {
        }

        public ProductAttribute(string code, string frontendInput = "text", bool isRequired = false)
        {
            this.AttributeCode = code;
            this.FrontendInput = frontendInput;
            this.IsRequired = false;
            this.EntityTypeId = EntityType.CatalogProduct;
        }
    }
}