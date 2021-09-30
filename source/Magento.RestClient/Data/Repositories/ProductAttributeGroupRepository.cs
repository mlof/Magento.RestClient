using System.Linq;
using Magento.RestClient.Data.Models.Attributes;
using Magento.RestClient.Data.Repositories.Abstractions;
using Magento.RestClient.Expressions;
using RestSharp;

namespace Magento.RestClient.Data.Repositories
{
	internal class ProductAttributeGroupRepository : IProductAttributeGroupRepository
	{
		private readonly IRestClient _client;

		public ProductAttributeGroupRepository(IRestClient client)
		{
			_client = client;
		}


		public IQueryable<AttributeGroup> AsQueryable()
		{
			return new MagentoQueryable<AttributeGroup>(_client, "products/attribute-sets/groups/list");
		}
	}
}