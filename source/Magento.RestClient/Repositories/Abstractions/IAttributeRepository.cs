using System.Collections.Generic;
using Magento.RestClient.Domain;
using Magento.RestClient.Models;
using Magento.RestClient.Models.Attributes;
using Magento.RestClient.Models.Products;

namespace Magento.RestClient.Repositories.Abstractions
{
    public interface IAttributeRepository
    {
        
        IEnumerable<EntityAttribute> GetProductAttributes(long attributeSetId);

        ProductAttribute Create(ProductAttribute attribute);
        void DeleteProductAttribute(string attributeCode);
        List<Option> GetProductAttributeOptions(string attributeCode);
        int CreateProductAttributeOption(string attributeCode, Option option);
        ProductAttribute GetByCode(string code);
        ProductAttribute Update(string attributeCode, ProductAttribute attribute);
        void DeleteProductAttributeOption(string attributeCode, string optionValue);
    }
}