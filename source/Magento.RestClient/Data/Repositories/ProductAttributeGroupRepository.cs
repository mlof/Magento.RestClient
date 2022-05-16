using System.Linq;
using Magento.RestClient.Abstractions.Abstractions;
using Magento.RestClient.Abstractions.Repositories;
using Magento.RestClient.Data.Models.EAV.Attributes;
using Magento.RestClient.Expressions;

namespace Magento.RestClient.Data.Repositories
{
	internal class ProductAttributeGroupRepository : AbstractRepository, IProductAttributeGroupRepository
	{
		public ProductAttributeGroupRepository(IContext context) : base(context)
		{
		}

		public IQueryable<AttributeGroup> AsQueryable()
		{
			return new MagentoQueryable<AttributeGroup>(this.Client, "products/attribute-sets/groups/list");
		}
	}
}