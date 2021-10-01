using System.Linq;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Data.Models.Attributes;
using Magento.RestClient.Data.Repositories.Abstractions;
using Magento.RestClient.Expressions;
using RestSharp;

namespace Magento.RestClient.Data.Repositories
{
	internal class ProductAttributeGroupRepository :AbstractRepository,  IProductAttributeGroupRepository
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