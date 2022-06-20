using System.Linq;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Expressions;
using Magento.RestClient.Modules.Catalog.Abstractions;
using Magento.RestClient.Modules.EAV.Model;

namespace Magento.RestClient.Modules.Catalog
{
	internal class ProductAttributeGroupRepository : AbstractRepository, IProductAttributeGroupRepository
	{
		public ProductAttributeGroupRepository(IMagentoContext magentoContext) : base(magentoContext)
		{
		}

		public IQueryable<AttributeGroup> AsQueryable()
		{
			return new MagentoQueryable<AttributeGroup>(this.Client, "products/attribute-sets/groups/list");
		}
	}
}