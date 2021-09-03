using Magento.RestClient.Data.Models.Attributes;
using Magento.RestClient.Data.Models.Common;

namespace Magento.RestClient.Data.Models.Products
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