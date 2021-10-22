using Magento.RestClient.Abstractions.Abstractions;
using Magento.RestClient.Data.Models.EAV.Attributes;

namespace Magento.RestClient.Abstractions.Repositories
{
	public interface IProductAttributeGroupRepository : IHasQueryable<AttributeGroup>
	{
	}
}