using System.Collections.Generic;
using Magento.RestClient.Repositories.Abstractions.Customers;

namespace Magento.RestClient.Repositories.Abstractions
{
    public interface IReadAttributeSetRepository 
    {
        IEnumerable<EntityAttribute> GetProductAttributes(long attributeSetId);

    }
}