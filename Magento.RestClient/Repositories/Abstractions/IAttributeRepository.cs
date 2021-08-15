using System.Collections.Generic;
using Magento.RestClient.Domain;
using Magento.RestClient.Models;

namespace Magento.RestClient.Repositories.Abstractions
{
    public interface IAttributeRepository
    {
        
        IEnumerable<EntityAttribute> GetProductAttributes(long attributeSetId);

        ProductAttribute Create(ProductAttribute attribute);
        void DeleteProductAttribute(string attributeCode);
        List<Option> GetProductAttributeOptions(string attributeCode);
        int CreateProductAttributeOption(string attributeCode, Option option);
    }
}