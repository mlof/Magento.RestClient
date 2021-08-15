using System.Collections.Generic;
using Magento.RestClient.Search.Abstractions;

namespace Magento.RestClient.Repositories.Abstractions
{
    public interface IIntegrationClient
    {
        ISearchService Search { get; }
        IStoreRepository Stores { get; }
        IProductRepository Products { get; }
        IConfigurableProductRepository ConfigurableProducts { get; }
        IOrderRepository Orders { get; }
        ICustomerRepository Customers { get; }
        ICustomerGroupRepository CustomerGroups { get; }
        IDirectoryRepository Directory { get; }
        IAttributeSetRepository AttributeSets { get; }
        IInvoiceRepository Invoices { get; }
        ICategoryRepository Categories { get; }
        ICartRepository Carts { get; }
        IAttributeRepository Attributes { get; set; }
    }

    public interface IAttributeRepository
    {
        IEnumerable<EntityAttribute> GetProductAttributes(long attributeSetId);

        ProductAttribute Create(ProductAttribute attribute);
        void DeleteProductAttribute(string attributeCode);
        List<Option> GetProductAttributeOptions(string attributeCode);
        int CreateProductAttributeOption(string attributeCode, Option option);
    }

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
            EntityTypeId = EntityType.CatalogProduct;
        }
    }
}