using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Data.Models.Attributes;
using Magento.RestClient.Data.Models.Products;
using Magento.RestClient.Data.Repositories.Abstractions;
using Magento.RestClient.Exceptions;
using Magento.RestClient.Extensions;
using Microsoft.Extensions.Caching.Memory;
using RestSharp;

namespace Magento.RestClient.Data.Repositories
{
	internal class AttributeRepository : AbstractRepository, IAttributeRepository
	{
		public AttributeRepository(IContext context) : base(context)
		{
			this.RelativeExpiration = TimeSpan.FromSeconds(5);
		}

		public async Task<IEnumerable<EntityAttribute>> GetProductAttributes(long attributeSetId)
		{
			var request = new RestRequest("products/attribute-sets/{id}/attributes", Method.GET);
			request.AddOrUpdateParameter("id", attributeSetId, ParameterType.UrlSegment);
			request.SetScope("all");

			return await ExecuteAsync<List<EntityAttribute>>(request).ConfigureAwait(false);
		}

		public Task<ProductAttribute> Create(ProductAttribute attribute)
		{
			var request = new RestRequest("products/attributes", Method.POST);
			request.SetScope("all");

			request.AddJsonBody(new {attribute});

			return ExecuteAsync<ProductAttribute>(request);
		}

		public Task DeleteProductAttribute(string attributeCode)
		{
			var request = new RestRequest("products/attributes/{attributeCode}", Method.DELETE);
			var key = Client.BuildUri(request);
			Cache.Remove(key);
			request.AddOrUpdateParameter("attributeCode", attributeCode, ParameterType.UrlSegment);
			request.SetScope("all");

			return this.Client.ExecuteAsync(request);
		}

		public Task<List<Option>> GetProductAttributeOptions(string attributeCode)
		{
			var request = new RestRequest("products/attributes/{attributeCode}/options", Method.GET);

			request.AddOrUpdateParameter("attributeCode", attributeCode, ParameterType.UrlSegment);
			request.SetScope("all");

			return ExecuteAsync<List<Option>>(request);
		}

		public Task<int> CreateProductAttributeOption(string attributeCode, Option option)
		{
			var request = new RestRequest("products/attributes/{attributeCode}/options", Method.POST);

			request.AddOrUpdateParameter("attributeCode", attributeCode, ParameterType.UrlSegment);
			request.SetScope("all");

			request.AddJsonBody(new {option});
			return ExecuteAsync<int>(request);
		}

		async public Task<ProductAttribute> GetByCode(string attributeCode)
		{
			var request = new RestRequest("products/attributes/{attributeCode}", Method.GET);
			request.AddOrUpdateParameter("attributeCode", attributeCode, ParameterType.UrlSegment);
			request.SetScope("all");
			var key = Client.BuildUri(request);

			var cacheItem = Cache.Get<ProductAttribute>(key);

			if (cacheItem != null)
			{
				return cacheItem;
			}
			else
			{
				var result = await ExecuteAsync<ProductAttribute>(request).ConfigureAwait(false);
				Cache.Set(key, result, RelativeExpiration);

				return result;
			}
		}

		public TimeSpan RelativeExpiration { get; set; }

		public Task<ProductAttribute> Update(string attributeCode, ProductAttribute attribute)
		{
			var request = new RestRequest("products/attributes/{attributeCode}", Method.PUT);

			var key = Client.BuildUri(request);
			Cache.Remove(key);
			request.AddOrUpdateParameter("attributeCode", attributeCode, ParameterType.UrlSegment);
			request.SetScope("all");

			attribute.AttributeCode = null;
			request.AddJsonBody(new {attribute});

			return ExecuteAsync<ProductAttribute>(request);
		}

		public Task DeleteProductAttributeOption(string attributeCode, string optionValue)
		{
			var request = new RestRequest("products/attributes/{attributeCode}/options/{optionValue}", Method.DELETE);
			request.SetScope("all");
			request.AddOrUpdateParameter("attributeCode", attributeCode, ParameterType.UrlSegment);
			request.AddOrUpdateParameter("optionValue", optionValue, ParameterType.UrlSegment);
			return this.Client.ExecuteAsync(request);
		}

		public Task<ProductAttribute> GetById(long id)
		{
			var request = new RestRequest("products/attributes/{id}", Method.GET);
			request.AddOrUpdateParameter("id", id, ParameterType.UrlSegment);
			request.SetScope("all");

			return ExecuteAsync<ProductAttribute>(request);
		}
	}
}