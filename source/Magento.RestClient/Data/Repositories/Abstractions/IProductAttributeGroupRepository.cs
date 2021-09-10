using System.Linq;
using Magento.RestClient.Data.Models.Attributes;

namespace Magento.RestClient.Data.Repositories.Abstractions
{
	public interface IProductAttributeGroupRepository : IQueryable<AttributeGroup>
	{
	}
}