using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Magento.RestClient.Data.Models.Attributes;
using Magento.RestClient.Data.Repositories.Abstractions;
using Magento.RestClient.Expressions;
using RestSharp;

namespace Magento.RestClient
{
	internal class ProductAttributeGroupRepository : IProductAttributeGroupRepository
	{
		private readonly IRestClient _client;

		public ProductAttributeGroupRepository(IRestClient client)
		{
			_client = client;
		}

		private IQueryable<AttributeGroup> _productAttributeGroupRepositoryImplementation =>
			new MagentoQueryable<AttributeGroup>(_client, "products/attribute-sets/groups/list");

		public IEnumerator<AttributeGroup> GetEnumerator()
		{
			return this._productAttributeGroupRepositoryImplementation.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable) this._productAttributeGroupRepositoryImplementation).GetEnumerator();
		}

		public Type ElementType => this._productAttributeGroupRepositoryImplementation.ElementType;

		public Expression Expression => this._productAttributeGroupRepositoryImplementation.Expression;

		public IQueryProvider Provider => this._productAttributeGroupRepositoryImplementation.Provider;
	}
}