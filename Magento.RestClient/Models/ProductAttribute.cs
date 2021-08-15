using Magento.RestClient.Domain;

namespace Magento.RestClient.Models
{
    public class ProductAttribute : EntityAttribute
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