using Magento.RestClient.Abstractions;
using Magento.RestClient.Modules.EAV.Model;

namespace Magento.RestClient.Modules.Catalog.Abstractions
{
	public interface IProductAttributeGroupRepository : IHasQueryable<AttributeGroup>
	{
	}
}